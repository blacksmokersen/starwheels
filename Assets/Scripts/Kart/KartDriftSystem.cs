using UnityEngine;
using FX;

namespace Kart
{
    public class KartDriftSystem : MonoBehaviour
    {
        private KartStates kartStates;
        private TrailSystem trailSystem;

        private bool hasTurnedOtherSide;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
            trailSystem = GetComponentInChildren<TrailSystem>();
            trailSystem.HideTrail();
        }

        private void Update()
        {
            if (TurnSideDifferentFromDriftSide())
            {
                hasTurnedOtherSide = true;
            }
        }

        public void CheckNewTurnDirection()
        {           
            if (hasTurnedOtherSide && !TurnSideDifferentFromDriftSide())
            {
                EnterNextState();
            }
        }

        public bool TurnSideDifferentFromDriftSide()
        {
            return ((kartStates.DriftTurnState == DriftTurnStates.DriftingLeft && kartStates.TurningState == TurningStates.Right)
                || (kartStates.DriftTurnState == DriftTurnStates.DriftingRight && kartStates.TurningState == TurningStates.Left));
        }

        public void EnterNextState()
        {
            hasTurnedOtherSide = false;
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
            StartCoroutine(FindObjectOfType<KartPhysics>().Boost());
        }
    }
}
