using UnityEngine;
using FX;

namespace Kart
{
    public class KartDriftSystem : MonoBehaviour
    {
        private KartStates kartStates;
        private TrailSystem trailSystem;
        private TurningStates lastTurnState;

        private void Awake()
        {
            kartStates = GetComponentInParent<KartStates>();
            trailSystem = GetComponentInChildren<TrailSystem>();
            trailSystem.HideTrail();
        }

        public void CheckNewTurnDirection()
        {           
            if (lastTurnState != TurningStates.NotTurning && kartStates.TurningState != TurningStates.NotTurning 
                && lastTurnState != kartStates.TurningState)
            {
                EnterNextState();
            }
            lastTurnState = kartStates.TurningState;
        }

        public void EnterNextState()
        {
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
                    EnterTurbo();
                    break;
            }
        }

        private void EnterNormalDrift()
        {
            Debug.Log("Normal drift");
            trailSystem.ShowTrail();
            trailSystem.SetUniqueColor(Color.grey);
            kartStates.DriftBoostState = DriftBoostStates.SimpleDrift;
        }

        private void EnterOrangeDrift()
        {
            Debug.Log("Orange drift");
            trailSystem.SetUniqueColor(Color.yellow);
            kartStates.DriftBoostState = DriftBoostStates.OrangeDrift;
        }

        private void EnterRedDrift()
        {
            Debug.Log("Red drift");
            trailSystem.SetUniqueColor(Color.red);
            kartStates.DriftBoostState = DriftBoostStates.RedDrift;
        }

        private void EnterTurbo()
        {
            Debug.Log("Turbo drift");
            trailSystem.HideTrail();
            kartStates.DriftBoostState = DriftBoostStates.Turbo;
        }
    }
}
