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
        [Range(0, 100)] public float RequiredSpeedToDrift = 12f;

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
            if( kartStates.IsDrifting() && !HasRequiredSpeed())
            {
                StopDrift();
            }
        }
        #endregion

        #region Drift Logic

        public void InitializeDrift(float angle)
        {
            if (HasRequiredSpeed() && kartStates.IsGrounded() && angle != 0)
            {

                ResetDrift();

                KartEvents.Instance.OnDriftStart();

                if (angle < 0)
                {
                    KartEvents.Instance.OnDriftLeft();
                }
                if (angle > 0)
                {
                    KartEvents.Instance.OnDriftRight();
                }

                EnterNextState();
            }
        }

        public void StopDrift()
        {
            if (kartStates.IsDrifting())
            {
                if (kartStates.DriftState == DriftState.Red && HasRequiredSpeed())
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
            KartEvents.Instance.OnDriftReset();

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

            switch (kartStates.DriftState)
            {
                case DriftState.NotDrifting:
                    KartEvents.Instance.OnDriftWhite();
                    break;
                case DriftState.White:
                    KartEvents.Instance.OnDriftOrange();
                    break;
                case DriftState.Orange:
                    KartEvents.Instance.OnDriftRed();
                    break;
                case DriftState.Red:
                    break;
            }

            _driftedLongEnoughTimer = StartCoroutine(DriftTimer());
        }

        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(_kartEngine.Boost(BoostDuration, MagnitudeBoost, BoostSpeed));

            KartEvents.Instance.OnDriftBoostStart();
            yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
            KartEvents.Instance.OnDrfitBoostEnd();
        }
        #endregion
    }
}
