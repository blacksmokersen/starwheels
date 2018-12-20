using System.Collections;
using UnityEngine;
using Bolt;
using UnityEngine.Events;
using Common.PhysicsUtils;
using Steering;

namespace Drift
{
    public enum TurnState { NotTurning, Left, Right }
    public enum DriftState { NotDrifting, White, Orange, Red, Turbo }

    public class Drift : EntityBehaviour<IKartState>, IControllable
    {
        public DriftSettings Settings;
        public const float JoystickDeadZone1 = 0.1f;
        public const float JoystickDeadZone2 = 0.2f;

        [Header("States")]
        public TurnState TurningState = TurnState.NotTurning;
        public TurnState DriftTurnState = TurnState.NotTurning;
        public DriftState DriftState = DriftState.NotDrifting;

        [Header("Events")]
        public UnityEvent OnDriftStart;
        public UnityEvent OnDriftEnd;
        public UnityEvent OnDriftReset;

        public UnityEvent OnDriftLeft;
        public UnityEvent OnDriftRight;

        public UnityEvent OnDriftWhite;
        public UnityEvent OnDriftOrange;
        public UnityEvent OnDriftRed;

        public UnityEvent OnDriftBoostStart;
        public UnityEvent OnDriftBoostEnd;

        [SerializeField] private SteeringWheel _steeringWheel;
        [SerializeField] private GroundCondition _groundCondition;

        private float _currentTurnValue = 0f;
        private bool _hasTurnedOtherSide = false;
        private bool _driftedLongEnough = false;
        private Coroutine _driftedLongEnoughTimer;
        private Coroutine _physicsBoostCoroutine;
        private Rigidbody _rigidBody;

        // CORE

        private void Awake()
        {
            _rigidBody = GetComponentInParent<Rigidbody>();
        }

        private void Update()
        {
            if (entity.isControllerOrOwner)
            {
                SetTurnState(Input.GetAxis(Constants.Input.TurnAxis));

                if (IsDriftSideDifferentFromTurnSide())
                {
                    _hasTurnedOtherSide = true;
                }
                if (IsDrifting() && !HasRequiredSpeed())
                {
                    StopDrift();
                }

                if (DriftTurnState != TurnState.NotTurning)
                    _steeringWheel.enabled = false;
                else
                    _steeringWheel.enabled = true;

                MapInputs();
            }
        }

        private void FixedUpdate()
        {
            if (DriftTurnState != TurnState.NotTurning && HasRequiredSpeed())
            {
                ApplyDriftTorque();
                ApplyDriftSideForces();
            }
        }

        // PUBLIC

        public void DriftTurns(float turnValue)
        {
            if (!_groundCondition.Grounded)
            {
                StopDrift();
            }

            if (DriftTurnState != TurnState.NotTurning && HasRequiredSpeed())
            {
                CheckNewTurnDirection();
            }
            else if (DriftTurnState == TurnState.NotTurning && TurningState != TurnState.NotTurning)
            {
                InitializeDrift(turnValue);
            }
        }
        #region DriftBehaviour
        public void InitializeDrift(float angle)
        {
            if (IsDrifting()) return;
            if (!HasRequiredSpeed() || !_groundCondition.Grounded || angle == 0 || Input.GetButtonUp(Constants.Input.Drift)) return;

            ResetDrift();

            OnDriftStart.Invoke();

            if (angle < 0)
            {
                OnDriftLeft.Invoke();
                DriftTurnState = TurnState.Left;
            }
            if (angle > 0)
            {
                OnDriftRight.Invoke();
                DriftTurnState = TurnState.Right;
            }

            EnterNextState();
        }

        public void StopDrift()
        {
            if (DriftState == DriftState.Red && HasRequiredSpeed())
            {
                OnDriftBoostStart.Invoke();
                ResetDrift();
            }
            else
            {
                ResetDrift();
            }

            OnDriftEnd.Invoke();
        }

        public void ResetDrift()
        {
            DriftTurnState = TurnState.NotTurning;
            DriftState = DriftState.NotDrifting;
            OnDriftReset.Invoke();

            _driftedLongEnough = false;
            if (_driftedLongEnoughTimer != null)
            {
                StopCoroutine(_driftedLongEnoughTimer);
            }
        }
        #endregion

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
        public void ApplyDriftSideForces()
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

        public void ApplyDriftTorque()
        {
            float turnValueRestrain = _currentTurnValue;
            if (DriftTurnState == TurnState.Left)
            {
                turnValueRestrain = _currentTurnValue <= -JoystickDeadZone2 ? Settings.MaxInteriorAngle : _currentTurnValue >= JoystickDeadZone1 ? Settings.MaxExteriorAngle : 100;
                _currentTurnValue = _currentTurnValue <= -JoystickDeadZone2 ? _currentTurnValue : _currentTurnValue >= JoystickDeadZone1 ? _currentTurnValue : 1;
                _rigidBody.AddTorque(Vector3.up * (-turnValueRestrain * Mathf.Abs(_currentTurnValue)) * Settings.DriftTurnSpeed * Time.deltaTime);
            }
            else if (DriftTurnState == TurnState.Right)
            {
                turnValueRestrain = _currentTurnValue <= -JoystickDeadZone2 ? Settings.MaxExteriorAngle : _currentTurnValue >= JoystickDeadZone1 ? Settings.MaxInteriorAngle : 100;
                _currentTurnValue = _currentTurnValue <= -JoystickDeadZone2 ? _currentTurnValue : _currentTurnValue >= JoystickDeadZone1 ? _currentTurnValue : 1;
                _rigidBody.AddTorque(Vector3.up * (turnValueRestrain * Mathf.Abs(_currentTurnValue)) * Settings.DriftTurnSpeed * Time.deltaTime);
            }
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
                    DriftState = DriftState.White;
                    OnDriftWhite.Invoke();
                    break;
                case DriftState.White:
                    DriftState = DriftState.Orange;
                    OnDriftOrange.Invoke();
                    break;
                case DriftState.Orange:
                    DriftState = DriftState.Red;
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

            _currentTurnValue = turnValue;
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
    }
}
