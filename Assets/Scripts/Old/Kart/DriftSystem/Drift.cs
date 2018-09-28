using System.Collections;
using UnityEngine;
using System;
using Tools;
using Photon.Pun;



namespace drift
{
    public enum TurnState { NotTurning, Left, Right }
    public enum DriftState { NotDrifting, White, Orange, Red, Turbo }

    [RequireComponent(typeof(GroundCondition))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PhotonView))]
    public class Drift : MonoBehaviourPun, IControllable
    {
        public DriftSettings Settings;
        public const float JoystickDeadZone1 = 0.1f;
        public const float JoystickDeadZone2 = 0.2f;

        private bool _hasTurnedOtherSide = false;
        private bool _driftedLongEnough = false;
        private Coroutine _driftedLongEnoughTimer;
        private Coroutine _physicsBoostCoroutine;
        private Coroutine _turboCoroutine;
        private Rigidbody _rigidBody;
        private GroundCondition _groundCondition;

        public Action OnDriftStart;
        public Action OnDriftLeft;
        public Action OnDriftRight;
        public Action OnDriftWhite;
        public Action OnDriftOrange;
        public Action OnDriftRed;
        public Action OnDriftEnd;
        public Action OnDriftReset;


        public TurnState TurningState = TurnState.NotTurning;
        public TurnState DriftTurnState = TurnState.NotTurning;
        public DriftState DriftState = DriftState.NotDrifting;


        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _groundCondition = GetComponent<GroundCondition>();
        }

        public void DriftTurns(float turnValue)
        {

            if (!_groundCondition.Grounded) return;

            if (DriftTurnState != TurnState.NotTurning && HasRequiredSpeed())
            {
                DriftTurn(turnValue);
                DriftUsingForce();
                CheckNewTurnDirection();
            }
            else if (DriftTurnState == TurnState.NotTurning && TurningState != TurnState.NotTurning)
            {
                InitializeDrift(turnValue);
            }
        }

        public void DriftUsingForce()
        {
            if (DriftTurnState == TurnState.Left)
            {
                _rigidBody.AddRelativeForce(Vector3.right * Settings.DriftGlideOrientation, ForceMode.Force);
                _rigidBody.AddRelativeForce(Vector3.back * Settings.DriftGlideBack, ForceMode.Force);
            }
            else if (DriftTurnState == TurnState.Right)
            {
                _rigidBody.AddRelativeForce(Vector3.left * Settings.DriftGlideOrientation, ForceMode.Force);
                _rigidBody.AddRelativeForce(Vector3.back * Settings.DriftGlideBack, ForceMode.Force);
            }
        }

        public void DriftTurn(float turnValue)
        {
            float turnValueRestrain = turnValue;
            if (DriftTurnState == TurnState.Left)
            {
                turnValueRestrain = turnValue <= -JoystickDeadZone2 ? Settings.MaxInteriorAngle : turnValue >= JoystickDeadZone1 ? Settings.MaxExteriorAngle : 100;
                turnValue = turnValue <= -JoystickDeadZone2 ? turnValue : turnValue >= JoystickDeadZone1 ? turnValue : 1;
                _rigidBody.AddTorque(Vector3.up * (-turnValueRestrain * Mathf.Abs(turnValue)) * Settings.DriftTurnSpeed * Time.deltaTime);
            }
            else if (DriftTurnState == TurnState.Right)
            {
                turnValueRestrain = turnValue <= -JoystickDeadZone2 ? Settings.MaxExteriorAngle : turnValue >= JoystickDeadZone1 ? Settings.MaxInteriorAngle : 100;
                turnValue = turnValue <= -JoystickDeadZone2 ? turnValue : turnValue >= JoystickDeadZone1 ? turnValue : 1;
                _rigidBody.AddTorque(Vector3.up * (turnValueRestrain * Mathf.Abs(turnValue)) * Settings.DriftTurnSpeed * Time.deltaTime);
            }
        }

        public void InitializeDrift(float angle)
        {
            if (IsDrifting()) return;
            if (!HasRequiredSpeed() || !_groundCondition.Grounded || angle == 0) return;

            ResetDrift();

            CallRPC("OnDriftStart");

            if (angle < 0)
            {
                OnDriftLeft();
            }
            if (angle > 0)
            {
                OnDriftRight();
            }

            EnterNextState();
        }


        public void StopDrift()
        {
            if (!IsDrifting()) return;

            if (DriftState == DriftState.Red && HasRequiredSpeed())
            {
                // LANCER EVENT BOOST
            }
            else
            {
                ResetDrift();
            }

            CallRPC("OnDriftEnd");
        }



        public void ResetDrift()
        {
            CallRPC("OnDriftReset");

            _driftedLongEnough = false;
            if (_driftedLongEnoughTimer != null)
            {
                StopCoroutine(_driftedLongEnoughTimer);
            }

            if (_turboCoroutine != null)
            {
                StopCoroutine(_turboCoroutine);
            }
        }

        #region Conditions
        public void CheckNewTurnDirection()
        {
            if (_hasTurnedOtherSide && IsDriftSideEqualsTurnSide() && _driftedLongEnough)
            {
                EnterNextState();
            }
        }

        public bool HasRequiredSpeed()
        {
            var playerVelocity = transform.InverseTransformDirection(_rigidBody.velocity).z;
            return playerVelocity >= Settings.RequiredSpeedToDrift;
        }
        #endregion


        #region Changing States
        private void EnterNextState()
        {
            _hasTurnedOtherSide = false;
            _driftedLongEnough = false;

            switch (DriftState)
            {
                case DriftState.NotDrifting:
                    CallRPC("OnDriftWhite");
                    break;
                case DriftState.White:
                    CallRPC("OnDriftOrange");
                    break;
                case DriftState.Orange:
                    CallRPC("OnDriftRed");
                    break;
                case DriftState.Red:
                    break;
            }

            _driftedLongEnoughTimer = StartCoroutine(DriftTimer());
        }

        public bool IsDriftSideEqualsTurnSide()
        {
            if (TurningState == TurnState.NotTurning || DriftTurnState == TurnState.NotTurning)
            {
                return false;
            }

            return TurningState == DriftTurnState;
        }

        public bool IsDriftSideDifferentFromTurnSide()
        {
            if (TurningState == TurnState.NotTurning || DriftTurnState == TurnState.NotTurning)
            {
                return false;
            }

            return TurningState != DriftTurnState;
        }


        public bool IsDrifting()
        {
            return DriftTurnState != TurnState.NotTurning;
        }

        #endregion

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(Settings.TimeBetweenDrifts);
            _driftedLongEnough = true;
        }

        public void MapInputs()
        {
            DriftTurns(Input.GetAxis(Constants.Input.TurnAxis));
        }


        [PunRPC] public void RPCOnDriftStart() { OnDriftStart(); }
        [PunRPC] public void RPCOnDriftWhite() { OnDriftWhite(); }
        [PunRPC] public void RPCOnDriftOrange() { OnDriftOrange(); }
        [PunRPC] public void RPCOnDriftRed() { OnDriftRed(); }
        [PunRPC] public void RPCOnDriftEnd() { OnDriftEnd(); }
        [PunRPC] public void RPCOnDriftReset() { OnDriftReset(); }

        public void CallRPC(string onAction, params object[] parameters)
        {
            photonView.RPC("RPC" + onAction, RpcTarget.All, parameters);
        }
    }
}
