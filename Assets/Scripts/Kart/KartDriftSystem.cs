using System.Collections;
using UnityEngine;
using MyExtensions;

namespace Kart
{
    public class KartDriftSystem : PunBaseKartComponent
    {
        [Header("Time")]
        [Range(0, 10)] public float TimeBetweenDrifts;
        [Range(0, 10)] public float BoostDuration;

        [Header("Speed")]
        [Range(0, 1000)] public float BoostSpeed;
        [Range(0, 30)] public float MagnitudeBoost;

        [Header("Angles")]
        [Range(0, 90)] public float ForwardMaxAngle;
        [Range(0, -90)] public float BackMaxAngle;

        private KartStates kartStates;
        private KartEngine kartPhysics;
        private CinemachineDynamicScript cinemachineDynamicScript;
        private KartEngine kartEngine;

        private bool hasTurnedOtherSide = false;
        private bool driftedLongEnough;
        private Coroutine driftedLongEnoughTimer;
        private Coroutine physicsBoostCoroutine;
        private Coroutine turboCoroutine;

        private new void Awake()
        {
            base.Awake();
            kartEngine = GetComponent<KartEngine>();
            kartStates = GetComponentInParent<KartStates>();
        }

        private void Update()
        {
            if (DriftSideDifferentFromTurnSide())
            {
                hasTurnedOtherSide = true;
            }
        }

        public void DriftForces()
        {
            kartEngine.DriftUsingForce();
        }

        public void CheckNewTurnDirection()
        {
            if (hasTurnedOtherSide && DriftSideEqualsTurnSide() && driftedLongEnough)
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
            return (kartStates.TurningState == TurningStates.Left && kartStates.DriftTurnState == TurningStates.Right) ||
                (kartStates.TurningState == TurningStates.Right && kartStates.DriftTurnState == TurningStates.Left);
        }

        public void EnterNextState()
        {
            hasTurnedOtherSide = false;
            driftedLongEnough = false;
            switch (kartStates.DriftBoostState)
            {
                case DriftBoostStates.NotDrifting:
                    EnterNormalDrift();
                    break;
                case DriftBoostStates.SimpleDrift:
                    EnterOrangeDrift();
                    break;
                case DriftBoostStates.OrangeDrift:
                    EnterRedDrift();
                    break;
                case DriftBoostStates.RedDrift:
                    break;
            }
            driftedLongEnoughTimer = StartCoroutine(DriftTimer());
        }

        public void InitializeDrift(float angle)
        {
            if (turboCoroutine != null)
            {
                StopCoroutine(turboCoroutine);
                SetKartBoostState(DriftBoostStates.NotDrifting, ColorId.Gray);
            }

            if (kartStates.DriftBoostState == DriftBoostStates.NotDrifting)
            {
                if (angle < 0)
                {
                    SetKartTurnState(TurningStates.Left);
                }
                if (angle > 0)
                {
                    SetKartTurnState(TurningStates.Right);
                }
                EnterNextState();
            }
        }

        public void StopDrift()
        {
            if (kartStates.DriftBoostState == DriftBoostStates.RedDrift)
            {
                turboCoroutine = StartCoroutine(EnterTurbo());
            }
            else
            {
                ResetDrift();
            }
        }

        private void EnterNormalDrift()
        {
            SetKartBoostState(DriftBoostStates.SimpleDrift, ColorId.Gray);
            kartEvents.OnDriftStart();
        }

        private void EnterOrangeDrift()
        {
            SetKartBoostState(DriftBoostStates.OrangeDrift, ColorId.Yellow);
            kartEvents.OnDriftOrange();
        }

        private void EnterRedDrift()
        {
            SetKartBoostState(DriftBoostStates.RedDrift, ColorId.Red);
            kartEvents.OnDriftRed();
        }

        private IEnumerator EnterTurbo()
        {
            if (physicsBoostCoroutine != null) StopCoroutine(physicsBoostCoroutine);
            kartEvents.OnDriftBoost();
            physicsBoostCoroutine = StartCoroutine(kartEngine.Boost(BoostDuration, MagnitudeBoost, BoostSpeed));            
            SetKartBoostState(DriftBoostStates.Turbo, ColorId.Green);
            SetKartTurnState(TurningStates.NotTurning);
            yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
            yield break;
        }

        public void ResetDrift()
        {
            kartEvents.OnDriftReset();
            SetKartTurnState(TurningStates.NotTurning);
            SetKartBoostState(DriftBoostStates.NotDrifting, ColorId.Gray);
            driftedLongEnough = false;
            if (driftedLongEnoughTimer != null)
            {
                StopCoroutine(driftedLongEnoughTimer);
            }
        }

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(TimeBetweenDrifts);
            driftedLongEnough = true;
        }

        private void SetKartBoostState(DriftBoostStates state, ColorId colorId)
        {
            this.ExecuteRPC(PhotonTargets.All, "RPCSetKartBoostState", state, colorId);
        }

        private void SetKartTurnState(TurningStates state)
        {
            this.ExecuteRPC(PhotonTargets.All, "RPCSetKartTurnState", state);
        }

        private enum ColorId
        {
            Red, Yellow, Gray, Green
        }

        private Color ColorIdToColor(ColorId colorId)
        {
            switch (colorId)
            {
                case ColorId.Gray:
                    return Color.gray;
                case ColorId.Red:
                    return Color.red;
                case ColorId.Green:
                    return Color.green;
                case ColorId.Yellow:
                    return Color.yellow;
            }
            return Color.white;
        }

        [PunRPC]
        private void RPCSetKartBoostState(DriftBoostStates state, ColorId colorId)
        {
            kartStates.DriftBoostState = state;
        }

        [PunRPC]
        private void RPCSetKartTurnState(TurningStates state)
        {
            kartStates.DriftTurnState = state;
        }
    }
}
