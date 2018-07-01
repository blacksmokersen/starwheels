using UnityEngine;
using FX;
using System.Collections;

namespace Kart
{
    public class KartDriftSystem : MonoBehaviour
    {
        public float TimeBetweenDrifts;
        public float BoostDuration;

        private KartStates kartStates;
        private KartPhysics kartPhysics;
        private ParticlesController particlesController;

        private bool hasTurnedOtherSide;
        private bool driftedLongEnough;
        private Coroutine driftTimer;

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
            if (hasTurnedOtherSide && !TurnSideDifferentFromDriftSide() && driftedLongEnough)
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
            driftTimer = StartCoroutine(DriftTimer());
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
            EnterNextState();
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
                ResetDrift();
            }
        }

        public void ResetDrift()
        {
            kartStates.DriftTurnState = DriftTurnStates.NotDrifting;
            kartStates.DriftBoostState = DriftBoostStates.NotDrifting;
            driftedLongEnough = false;
            hasTurnedOtherSide = false;
            particlesController.Hide();
            if (driftTimer != null)
            {
                StopCoroutine(driftTimer);
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
            StartCoroutine(kartPhysics.Boost(BoostDuration));
            kartStates.DriftBoostState = DriftBoostStates.Turbo;
            kartStates.DriftTurnState = DriftTurnStates.NotDrifting;
            yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
        }

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(TimeBetweenDrifts);
            driftedLongEnough = true;
        }


    }
}
