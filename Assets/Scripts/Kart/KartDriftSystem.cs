using System.Collections;
using UnityEngine;

namespace Kart
{
    public class KartDriftSystem : BaseKartComponent
    {
        [Header("Condition")]
        [Range(0, 100)] public float requiredSpeed = 10f;

        [Header("Time")]
        [Range(0, 10)] public float TimeBetweenDrifts;
        [Range(0, 10)] public float BoostDuration;

        [Header("Speed")]
        [Range(0, 1000)] public float BoostSpeed;
        [Range(0, 30)] public float MagnitudeBoost;

        [Header("Angles")]
        [Range(0, 90)] public float ForwardMaxAngle;
        [Range(0, -90)] public float BackMaxAngle;

        private KartEngine _kartEngine;
        private bool _hasTurnedOtherSide = false;
        private bool _driftedLongEnough;
        private Coroutine _driftedLongEnoughTimer;
        private Coroutine _physicsBoostCoroutine;
        private Coroutine _turboCoroutine;

        // CORE

        private new void Awake()
        {
            base.Awake();

            _kartEngine = GetComponentInChildren<KartEngine>();
        }

        private void Update()
        {
            if (DriftSideDifferentFromTurnSide())
            {
                _hasTurnedOtherSide = true;
            }
        }

        // PUBLIC

        public void DriftForces()
        {
            _kartEngine.DriftUsingForce();
        }

        public void CheckNewTurnDirection()
        {
            if (_hasTurnedOtherSide && DriftSideEqualsTurnSide() && _driftedLongEnough)
            {
                EnterNextState();
            }
        }

        public bool DriftSideEqualsTurnSide()
        {
            return kartStates.TurningState == kartStates.DriftTurnState;
        }

        public bool DriftSideDifferentFromTurnSide()
        {
            if (kartStates.TurningState == TurnState.NotTurning || kartStates.DriftTurnState == TurnState.NotTurning)
            {
                return false;
            }

            return kartStates.TurningState != kartStates.DriftTurnState;
        }

        public void EnterNextState()
        {
            _hasTurnedOtherSide = false;
            _driftedLongEnough = false;

            switch (kartStates.DriftBoostState)
            {
                case DriftBoostState.NotDrifting:
                    EnterNormalDrift();
                    break;
                case DriftBoostState.Simple:
                    EnterOrangeDrift();
                    break;
                case DriftBoostState.Orange:
                    EnterRedDrift();
                    break;
                case DriftBoostState.Red:
                    break;
            }

            _driftedLongEnoughTimer = StartCoroutine(DriftTimer());
        }

        public void InitializeDrift(float angle)
        {
            if (CheckRequiredSpeed() && kartStates.IsGrounded() && angle > 0) return;

            if (_turboCoroutine != null)
            {
                StopCoroutine(_turboCoroutine);
                SetBoostState(DriftBoostState.NotDrifting);
            }

            if (kartStates.DriftBoostState == DriftBoostState.NotDrifting)
            {
                if (angle < 0)
                {
                    SetTurnState(TurnState.Left);
                    KartEvents.Instance.OnDriftLeft();
                }
                if (angle > 0)
                {
                    SetTurnState(TurnState.Right);
                    KartEvents.Instance.OnDriftRight();
                }
                EnterNextState();
            }
        }

        public bool CheckRequiredSpeed()
        {
            return _kartEngine.PlayerVelocity >= requiredSpeed;
        }

        public void StopDrift()
        {
            if (kartStates.DriftBoostState == DriftBoostState.Red)
            {
                _turboCoroutine = StartCoroutine(EnterTurbo());
            }
            else
            {
                KartEvents.Instance.OnDriftEnd();
                ResetDrift();
            }
        }

        public void ResetDrift()
        {
            KartEvents.Instance.OnDriftReset();
            SetTurnState(TurnState.NotTurning);
            SetBoostState(DriftBoostState.NotDrifting);

            _driftedLongEnough = false;
            if (_driftedLongEnoughTimer != null)
            {
                StopCoroutine(_driftedLongEnoughTimer);
            }
        }

        // PRIVATE

        private void EnterNormalDrift()
        {
            SetBoostState(DriftBoostState.Simple);
            KartEvents.Instance.OnDriftStart();
        }

        private void EnterOrangeDrift()
        {
            SetBoostState(DriftBoostState.Orange);
            KartEvents.Instance.OnDriftOrange();
        }

        private void EnterRedDrift()
        {
            SetBoostState(DriftBoostState.Red);
            KartEvents.Instance.OnDriftRed();
        }

        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null) StopCoroutine(_physicsBoostCoroutine);
            KartEvents.Instance.OnDriftBoost();
            KartEvents.Instance.OnDriftEnd();
            _physicsBoostCoroutine = StartCoroutine(_kartEngine.Boost(BoostDuration, MagnitudeBoost, BoostSpeed));
            SetBoostState(DriftBoostState.Turbo);
            SetTurnState(TurnState.NotTurning);
            yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
            yield break;
        }

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(TimeBetweenDrifts);
            _driftedLongEnough = true;
        }

        private void SetBoostState(DriftBoostState state)
        {
            kartStates.DriftBoostState = state;
        }

        private void SetTurnState(TurnState state)
        {
            kartStates.DriftTurnState = state;
        }
    }
}
