using System.Collections;
using UnityEngine;

namespace Kart
{
    public class KartDriftSystem : BaseKartComponent
    {
        [Header("Time")]
        [Range(0, 10)] public float TimeBetweenDrifts;
        [Range(0, 10)] public float BoostDuration;

        [Header("Speed")]
        [Range(0, 1000)] public float BoostSpeed;
        [Range(0, 30)] public float MagnitudeBoost;
        [Range(0, 100)] public float RequiredSpeedToDrift = 20f;

        [Header("Angles")]
        [Range(0, 90)] public float ForwardMaxAngle;
        [Range(0, -90)] public float BackMaxAngle;

        private KartEngine _kartEngine;
        private bool _hasTurnedOtherSide = false;
        private bool _driftedLongEnough = false;
        private Coroutine _driftedLongEnoughTimer;
        private Coroutine _physicsBoostCoroutine;
        private Coroutine _turboCoroutine;

        #region MonoBehaviour

        private new void Awake()
        {
            base.Awake();

            _kartEngine = GetComponentInChildren<KartEngine>();

            kartEvents.OnHit += StopDrift;
        }

        private void Update()
        {
            if (kartStates.IsDriftSideDifferentFromTurnSide())
            {
                _hasTurnedOtherSide = true;
            }
        }
        #endregion

        #region Drift Logic

        public void InitializeDrift(float angle)
        {
            if (HasRequiredSpeed() && kartStates.IsGrounded() && angle != 0)
            {
                ResetDrift();

                if (angle < 0)
                {
                    kartStates.SetDriftTurnState(TurnState.Left);
                    KartEvents.Instance.OnDriftLeft();
                }
                if (angle > 0)
                {
                    kartStates.SetDriftTurnState(TurnState.Right);
                    KartEvents.Instance.OnDriftRight();
                }
                EnterNextState();
            }
        }

        public void StopDrift()
        {
            if (kartStates.IsDrifting())
            {
                if (kartStates.DriftBoostState == DriftBoostState.Red)
                {
                    _turboCoroutine = StartCoroutine(EnterTurbo());
                }
                else
                {
                    ResetDrift();
                }
                KartEvents.Instance.OnDriftEnd();
            }
        }

        public void ResetDrift()
        {
            kartStates.SetDriftTurnState(TurnState.NotTurning);
            kartStates.SetDriftBoostState(DriftBoostState.NotDrifting);

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

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(TimeBetweenDrifts);
            _driftedLongEnough = true;
        }

        #endregion

        #region Conditions
        public void CheckNewTurnDirection()
        {
            if (_hasTurnedOtherSide && kartStates.IsDriftSideEqualsTurnSide() && _driftedLongEnough)
            {
                EnterNextState();
            }
        }

        public bool HasRequiredSpeed()
        {
            return _kartEngine.PlayerVelocity >= RequiredSpeedToDrift;
        }
        #endregion

        #region Changing States
        private void EnterNextState()
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

        private void EnterNormalDrift()
        {
            kartStates.SetDriftBoostState(DriftBoostState.Simple);
            KartEvents.Instance.OnDriftStart();
        }

        private void EnterOrangeDrift()
        {
            kartStates.SetDriftBoostState(DriftBoostState.Orange);
            KartEvents.Instance.OnDriftOrange();
        }

        private void EnterRedDrift()
        {
            kartStates.SetDriftBoostState(DriftBoostState.Red);
            KartEvents.Instance.OnDriftRed();
        }

        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(_kartEngine.Boost(BoostDuration, MagnitudeBoost, BoostSpeed));
            kartStates.SetDriftTurnState(TurnState.NotTurning);
            kartStates.SetDriftBoostState(DriftBoostState.Turbo);

            KartEvents.Instance.OnDriftBoost();
            yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
        }
        #endregion
    }
}
