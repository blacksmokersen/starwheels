using System.Collections;
using UnityEngine;
using FX;
using MyExtensions;

namespace Kart
{
    public class KartDriftSystem : MonoBehaviour
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
        private KartPhysics kartPhysics;
        private ParticlesController particlesController;

        private bool hasTurnedOtherSide;
        private bool driftedLongEnough;
        private Coroutine driftTimer;

        // Drift Wydman
        public float OnDrift;
        private Rigidbody rb;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
            kartPhysics = GetComponent<KartPhysics>();
          //  particlesController = GetComponentInChildren<ParticlesController>();
          //  particlesController.Hide();
            rb = kartPhysics.rb;
        }

        private void Update()
        {
            if (TurnSideDifferentFromDriftSide())
            {
                hasTurnedOtherSide = true;
            }
        }

        public void DriftForces()
        {
            kartPhysics.DriftUsingForce();
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
            if (kartStates.DriftBoostState == DriftBoostStates.NotDrifting)
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
              //  particlesController.Show();
            }
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
          // hasTurnedOtherSide = false;
          //  particlesController.Hide();
            if (driftTimer != null)
            {
                StopCoroutine(driftTimer);
            }
        }

        private void EnterNormalDrift()
        {
         //   particlesController.SetColor(Color.grey);
            kartStates.DriftBoostState = DriftBoostStates.SimpleDrift;
        }

        private void EnterOrangeDrift()
        {
         //   particlesController.SetColor(Color.yellow);
            kartStates.DriftBoostState = DriftBoostStates.OrangeDrift;
        }

        private void EnterRedDrift()
        {
         //  particlesController.SetColor(Color.red);
            kartStates.DriftBoostState = DriftBoostStates.RedDrift;
        }

        private IEnumerator EnterTurbo()
        {
          //  particlesController.SetColor(Color.green);
            StartCoroutine(kartPhysics.Boost(BoostDuration, MagnitudeBoost, BoostSpeed));
            kartStates.DriftBoostState = DriftBoostStates.Turbo;
            kartStates.DriftTurnState = DriftTurnStates.NotDrifting;
          //  yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
            yield break;
        }

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(TimeBetweenDrifts);
            driftedLongEnough = true;
        }
    }
}
