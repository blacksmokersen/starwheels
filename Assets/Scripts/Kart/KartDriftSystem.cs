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
        private KartEffects kartEffects;

        private bool hasTurnedOtherSide;
        private bool driftedLongEnough;
        private Coroutine driftTimer;
        private Coroutine boostCoroutine;
        // Drift Wydman
        public float OnDrift;
        private Rigidbody rb;

        private Coroutine _turboCoroutine;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
            kartPhysics = GetComponent<KartPhysics>();
            kartEffects = GetComponentInChildren<KartEffects>();
            kartEffects.StopSmoke();
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
            if (_turboCoroutine != null)
            {
                StopCoroutine(_turboCoroutine);
                SetKartBoostState(DriftBoostStates.NotDrifting, ColorId.Gray);
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
            SetKartBoostState(DriftBoostStates.NotDrifting, ColorId.Gray);
            driftedLongEnough = false;
            // hasTurnedOtherSide = false;
            kartEffects.StopSmoke();
            if (driftTimer != null)
            {
                StopCoroutine(driftTimer);
            }
        }

        private void EnterNormalDrift()
        {
            SetKartBoostState(DriftBoostStates.SimpleDrift, ColorId.Gray);
        }

        private void EnterOrangeDrift()
        {
            SetKartBoostState(DriftBoostStates.OrangeDrift, ColorId.Yellow);
        }

        private void EnterRedDrift()
        {
            SetKartBoostState(DriftBoostStates.RedDrift, ColorId.Red);
        }

        private IEnumerator EnterTurbo()
        {
            if (boostCoroutine != null) StopCoroutine(boostCoroutine);
            boostCoroutine = StartCoroutine(kartPhysics.Boost(BoostDuration, MagnitudeBoost, BoostSpeed));
            SetKartBoostState(DriftBoostStates.Turbo, ColorId.Green);
            SetKartTurnState(DriftTurnStates.NotDrifting);
            yield return new WaitForSeconds(BoostDuration);
            ResetDrift();
            yield break;
        }

        private IEnumerator DriftTimer()
        {
            yield return new WaitForSeconds(TimeBetweenDrifts);
            driftedLongEnough = true;
        }

        private void SetKartBoostState(DriftBoostStates state, ColorId colorId)
        {
            if (PhotonNetwork.connected)
                photonView.RPC("RPCSetKartBoostState", PhotonTargets.AllBuffered, state, colorId);
            else
            {
                RPCSetKartBoostState(state, colorId);
            }
        }

        private void SetKartTurnState(DriftTurnStates state)
        {
            if (PhotonNetwork.connected)
                photonView.RPC("RPCSetKartTurnState", PhotonTargets.AllBuffered, state);
            else
                kartStates.DriftTurnState = state;
        }

        [PunRPC]
        private void RPCSetKartBoostState(DriftBoostStates state, ColorId colorId)
        {
            kartStates.DriftBoostState = state;

            if (state != DriftBoostStates.NotDrifting)
                kartEffects.StartSmoke();
            else
                kartEffects.StopSmoke();

            kartEffects.SetColor(ColorIdToColor(colorId));
        }

        [PunRPC]
        private void RPCSetKartTurnState(DriftTurnStates state)
        {
            kartStates.DriftTurnState = state;
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
    }
}
