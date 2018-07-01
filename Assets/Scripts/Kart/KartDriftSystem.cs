using UnityEngine;
using FX;
using System.Collections;

namespace Kart
{
    public class KartDriftSystem : MonoBehaviour
    {
        private KartStates kartStates;
        private KartPhysics kartPhysics;
        private ParticlesController particlesController;

        private bool hasTurnedOtherSide;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
            kartPhysics = GetComponent<KartPhysics>();
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
            if (kartStates.DriftBoostState == DriftBoostStates.RedDrift)
            {
                StartCoroutine(EnterTurbo());
            }
            else
            {
                kartStates.DriftTurnState = DriftTurnStates.NotDrifting;
                kartStates.DriftBoostState = DriftBoostStates.NotDrifting;
                particlesController.Hide();
            }
        }

        private void EnterNormalDrift()
        {
            particlesController.SetColor(Color.grey);
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

        private IEnumerator EnterTurbo()
        {
            Debug.Log("Turbo drift");
            particlesController.SetColor(Color.green);
            StartCoroutine(kartPhysics.Boost());
            kartStates.DriftBoostState = DriftBoostStates.Turbo;
            kartStates.DriftTurnState = DriftTurnStates.NotDrifting;
            yield return new WaitForSeconds(2.0f);
            kartStates.DriftBoostState = DriftBoostStates.NotDrifting;
            particlesController.Hide();
        }


    }
}
