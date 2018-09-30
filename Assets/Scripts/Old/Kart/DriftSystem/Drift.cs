using System.Collections;
using UnityEngine;
using System;
using Tools;
using Photon.Pun;
using UnityEngine.Events;



namespace drift
{
    public enum TurnState { NotTurning, Left, Right }
    public enum DriftState { NotDrifting, White, Orange, Red, Turbo }

    [RequireComponent(typeof(GroundCondition))]
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
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private GroundCondition _groundCondition;

        public TurnState TurningState = TurnState.NotTurning;
        public TurnState DriftTurnState = TurnState.NotTurning;
        public DriftState DriftState = DriftState.NotDrifting;

        public UnityEvent OnDriftStart;
        public UnityEvent OnDriftLeft;
        public UnityEvent OnDriftRight;
        public UnityEvent OnDriftWhite;
        public UnityEvent OnDriftOrange;
        public UnityEvent OnDriftRed;
        public UnityEvent OnDriftEnd;
        public UnityEvent OnDriftReset;
        public UnityEvent OnDriftBoostStart;
        public UnityEvent OnDriftBoostEnd;

        private void Awake()
        {

            /*
            OnDriftLeft += () => DriftTurnState = TurnState.Left;
            OnDriftRight += () => DriftTurnState = TurnState.Right;

            OnDriftWhite += () => DriftState = DriftState.White;
            OnDriftOrange += () => DriftState = DriftState.Orange;
            OnDriftRed += () => DriftState = DriftState.Red;

            OnDriftReset += () => DriftTurnState = TurnState.NotTurning;
            OnDriftReset += () => DriftState = DriftState.NotDrifting;
            */

            OnDriftLeft.AddListener(() => { DriftTurnState = TurnState.Left; });
            OnDriftRight.AddListener(() => { DriftTurnState = TurnState.Right; });

            OnDriftWhite.AddListener(() => { DriftState = DriftState.White; });
            OnDriftOrange.AddListener(() => { DriftState = DriftState.Orange; });
            OnDriftRed.AddListener(() => { DriftState = DriftState.Red; });

            OnDriftReset.AddListener(() => { DriftTurnState = TurnState.NotTurning; });
            OnDriftReset.AddListener(() => { DriftState = DriftState.NotDrifting; });

            OnDriftStart.AddListener(() => { });
        }


        private void Update()
        {
            MapInputs();
        }

        private void FixedUpdate()
        {
            SetTurnState(Input.GetAxis(Constants.Input.TurnAxis));
        }

        public void DriftTurns(float turnValue)
        {
            _groundCondition.CheckGrounded();
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

        public void InitializeDrift(float angle)
        {
            _groundCondition.CheckGrounded();
            if (IsDrifting()) return;
            if (!HasRequiredSpeed() || !_groundCondition.Grounded || angle == 0) return;

            ResetDrift();

            OnDriftStart.Invoke();

            if (angle < 0)
            {
                OnDriftLeft.Invoke();
            }
            if (angle > 0)
            {
                OnDriftRight.Invoke();
            }

            EnterNextState();
        }

        public void StopDrift()
        {
            if (!IsDrifting()) return;

            if (DriftState == DriftState.Red && HasRequiredSpeed())
            {
                OnDriftBoostStart.Invoke();
            }
            else
            {
                ResetDrift();
            }

            OnDriftEnd.Invoke();
        }

        public void ResetDrift()
        {
            OnDriftReset.Invoke();

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

        #region DriftPhysics
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
            Debug.Log("DriftUsingForce");
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
            Debug.Log("DriftTurn");
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
                    OnDriftWhite.Invoke();
                    break;
                case DriftState.White:
                    OnDriftOrange.Invoke();
                    break;
                case DriftState.Orange:
                    OnDriftRed.Invoke();
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

        public void SetTurnState(TurnState state)
        {
            TurningState = state;
        }

        public void SetTurnState(float turnValue)
        {
            if (turnValue > 0)
            {
                SetTurnState(TurnState.Right);
            }
            else if (turnValue < 0)
            {
                SetTurnState(TurnState.Left);
            }
            else
            {
                SetTurnState(TurnState.NotTurning);
            }
        }
        #endregion

        #region MapInput

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.Drift))
            {
                InitializeDrift(Input.GetAxis(Constants.Input.TurnAxis));
                OnDriftStart.Invoke();
            }


            if (Input.GetButton(Constants.Input.Drift))
            {
                DriftTurns(Input.GetAxis(Constants.Input.TurnAxis));
            }


            if (Input.GetButtonUp(Constants.Input.Drift))
            {
                StopDrift();
            }
        }

        #endregion

        #region IEnumeratos

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(Settings.TimeBetweenDrifts);
            _driftedLongEnough = true;
        }

        #endregion



        /*
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
        */
    }
}
