using UnityEngine;
using FX;

namespace Kart
{
    public class KartDriftSystem : MonoBehaviour
    {
        private KartStates kartStates;
        private ParticlesController particlesController;

        private bool hasTurnedOtherSide;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
            particlesController = GetComponentInChildren<ParticlesController>();
            particlesController.Hide();
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

        public void InitializeDrift(float angle)
        {
            if (angle < 0)
            {
                kartStates.DriftTurnState = DriftTurnStates.DriftingLeft;
            }
            if (angle > 0)
            {
                kartStates.DriftTurnState = DriftTurnStates.DriftingRight;
            }
            EnterNormalDrift();
            particlesController.Show();
        }

        public void StopDrift()
        {
            kartStates.DriftTurnState = DriftTurnStates.NotDrifting;
            kartStates.DriftBoostState = DriftBoostStates.NotDrifting;
            particlesController.Hide();
        }

        private void EnterNormalDrift()
        {
            particlesController.SetColor(Color.green);
            Debug.Log("Normal drift");
            kartStates.DriftBoostState = DriftBoostStates.SimpleDrift;
        }

        private void EnterOrangeDrift()
        {
            Debug.Log("Orange drift");
            particlesController.SetColor(Color.yellow);
            kartStates.DriftBoostState = DriftBoostStates.OrangeDrift;
        }

        private void EnterRedDrift()
        {
            Debug.Log("Red drift");
            particlesController.SetColor(Color.red);
            kartStates.DriftBoostState = DriftBoostStates.RedDrift;
        }

        private void EnterTurbo()
        {
            Debug.Log("Turbo drift");
            StartCoroutine(FindObjectOfType<KartPhysics>().Boost());
        }
    }
}
