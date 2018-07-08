using System.Collections;
using UnityEngine;
using Photon;
using FX;
using MyExtensions;

namespace Kart
{
    public class KartDriftSystem : PunBehaviour
    {
        [Header("Time")]
        [Range(0, 2)] public float TimeBetweenDrifts;
        [Range(0, 5)] public float BoostDuration;

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
        public float DriftGlideOrientation = 500f;
        public float DriftGlideBack = 500f;
        private Rigidbody rb;

        private Coroutine _turboCoroutine;

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

        public void DriftForces(float turnValue)
        {
          //  float angle = 0f;
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                if (turnValue != 0) //(Input.GetAxis(StaticsVariables.TurnAxis) > 0)
                {
                    rb.AddRelativeForce(Vector3.right * DriftGlideOrientation, ForceMode.Force);
                    rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
                }
                else
                {
                    rb.AddRelativeForce(Vector3.right * DriftGlideOrientation, ForceMode.Force);
                    rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
                }
                //  angle = Mathf.PI - Mathf.Deg2Rad * Functions.RemapValue(-1, 1, ForwardMaxAngle, BackMaxAngle, turnValue);
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                if (turnValue != 0) //(Input.GetAxis(StaticsVariables.TurnAxis) > 0)
                {
                    rb.AddRelativeForce(Vector3.left * DriftGlideOrientation, ForceMode.Force);
                    rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
                }
                else
                {
                    rb.AddRelativeForce(Vector3.left * DriftGlideOrientation, ForceMode.Force);
                    rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
                }
                //  angle = Mathf.Deg2Rad * Functions.RemapValue(-1, 1, BackMaxAngle, ForwardMaxAngle, turnValue);
            }
          //  var direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
          //  kartPhysics.DriftUsingForce(direction);
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
            if (_turboCoroutine != null)
            {
                StopCoroutine(_turboCoroutine);
                SetKartBoostState(DriftBoostStates.NotDrifting);
            }

            if (kartStates.DriftBoostState == DriftBoostStates.NotDrifting)
            {
                if (angle < 0)
                {
                    SetKartTurnState(DriftTurnStates.DriftingLeft);
                }
                if (angle > 0)
                {
                    SetKartTurnState(DriftTurnStates.DriftingRight);
                }
                EnterNextState();
              //  particlesController.Show();
            }
        }

        public void StopDrift()
        {
            if (kartStates.DriftBoostState == DriftBoostStates.RedDrift)
            {
                _turboCoroutine = StartCoroutine(EnterTurbo());
            }
            else
            {
                ResetDrift();
            }
        }

        public void ResetDrift()
        {
            SetKartTurnState(DriftTurnStates.NotDrifting);
            SetKartBoostState(DriftBoostStates.NotDrifting);
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
            SetKartBoostState(DriftBoostStates.SimpleDrift);
        }

        private void EnterOrangeDrift()
        {
         //   particlesController.SetColor(Color.yellow);
            SetKartBoostState(DriftBoostStates.OrangeDrift);
        }

        private void EnterRedDrift()
        {
         //  particlesController.SetColor(Color.red);
            SetKartBoostState(DriftBoostStates.RedDrift);
        }

        private IEnumerator EnterTurbo()
        {
          //  particlesController.SetColor(Color.green);
            StartCoroutine(kartPhysics.Boost(BoostDuration, MagnitudeBoost, BoostSpeed));
            SetKartBoostState(DriftBoostStates.Turbo);
            SetKartTurnState(DriftTurnStates.NotDrifting);
            yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
        }

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(TimeBetweenDrifts);
            driftedLongEnough = true;
        }

        private void SetKartBoostState(DriftBoostStates state)
        {
            if (PhotonNetwork.connected)
                photonView.RPC("RPCSetKartBoostState", PhotonTargets.AllBuffered, state);
            else
                kartStates.DriftBoostState = state;
        }

        private void SetKartTurnState(DriftTurnStates state)
        {
            if (PhotonNetwork.connected)
                photonView.RPC("RPCSetKartTurnState", PhotonTargets.AllBuffered, state);
            else
                kartStates.DriftTurnState = state;
        }
        
        [PunRPC]
        private void RPCSetKartBoostState(DriftBoostStates state)
        {
            kartStates.DriftBoostState = state;
        }

        [PunRPC]
        private void RPCSetKartTurnState(DriftTurnStates state)
        {
            kartStates.DriftTurnState = state;
        }
    }
}
