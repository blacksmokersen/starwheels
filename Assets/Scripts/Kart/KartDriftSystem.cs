using UnityEngine;
using FX;

namespace Kart
{
    public class KartDriftSystem : MonoBehaviour
    {

        private KartStates kartStates;
        private TrailSystem trailSystem;

        private void Awake()
        {
            kartStates = GetComponentInParent<KartStates>();
            trailSystem = GetComponentInChildren<TrailSystem>();
        }

        public void NewTurnDirection(float value)
        {
            if (value > 0)
            {
                if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
                {
                    EnterNextState();
                }
            }
            else if (value < 0)
            {
                if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
                {
                    EnterNextState();
                }
            }
        }

        private void EnterNextState()
        {
            switch (kartStates.DriftBoostState)
            {
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
            trailSystem.SetUniqueColor(Color.grey);
            kartStates.DriftBoostState = DriftBoostStates.SimpleDrift;
        }

        private void EnterOrangeDrift()
        {
            trailSystem.SetUniqueColor(Color.yellow);
            kartStates.DriftBoostState = DriftBoostStates.OrangeDrift;
        }

        private void EnterRedDrift()
        {
            trailSystem.SetUniqueColor(Color.red);
            kartStates.DriftBoostState = DriftBoostStates.RedDrift;
        }

        private void EnterTurbo()
        {
            trailSystem.SetUniqueColor(Color.black);
            kartStates.DriftBoostState = DriftBoostStates.Turbo;
        }
    }
}
