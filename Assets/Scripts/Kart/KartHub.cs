using UnityEngine;
using Abilities;
using Items;
using CameraUtils;
using Photon.Pun;

namespace Kart
{
    /*
     * Main class to map the inputs to the different kart actions
     * The states should handled and modified within this class
     */
    public class KartHub : MonoBehaviourPun
    {
        [HideInInspector] public KartStates kartStates;
        [HideInInspector] public KartEvents kartEvents;
        [HideInInspector] public KartEngine kartEngine;
        [HideInInspector] public KartDriftSystem kartDriftSystem;
        [HideInInspector] public KartInventory kartInventory;
        [HideInInspector] public KartHealthSystem kartHealthSystem;
        [HideInInspector] public Ability kartAbility;
        [HideInInspector] public CinemachineDynamicScript cinemachineDynamicScript;
        [HideInInspector] public KartGameMode kartGameMode;

        // CORE

        private void Awake()
        {
            kartStates = GetComponent<KartStates>();
            kartEvents = GetComponent<KartEvents>();

            kartEngine = GetComponentInChildren<KartEngine>();
            kartDriftSystem = GetComponentInChildren<KartDriftSystem>();
            kartInventory = GetComponentInChildren<KartInventory>();
            kartHealthSystem = GetComponentInChildren<KartHealthSystem>();
            cinemachineDynamicScript = GetComponentInChildren<CinemachineDynamicScript>();
            kartGameMode = GetComponentInChildren<KartGameMode>();
        }

        private void FixedUpdate()
        {
            if (kartStates.DriftTurnState == TurnState.NotTurning)
            {
                kartEngine.CompensateSlip();
            }
        }

        // PUBLIC

        public void UseItem(float verticalValue)
        {
            if (verticalValue > 0.3f)
            {
                UseItemForward();
            }
            else if (verticalValue < -0.3f)
            {
                UseItemBackward();
            }
            else
            {
                kartInventory.ItemAction(Direction.Default);
            }
        }

        public void UseItemForward()
        {
            kartInventory.ItemAction(Direction.Forward);
        }

        public void UseItemBackward()
        {
            kartInventory.ItemAction(Direction.Backward);
        }

        public void UseAbility(float xAxis, float yAxis)
        {
            if (kartAbility == null) kartAbility = GetComponentInChildren<Ability>();
            if (kartAbility == null) return;

            kartAbility.Use(xAxis, yAxis);
        }

        public void InitializeDrift(float angle)
        {
            kartDriftSystem.InitializeDrift(angle);
        }

        public void StopDrift()
        {
            kartDriftSystem.StopDrift();
        }

        public void DriftTurns(float turnValue)
        {
            if (!kartStates.IsGrounded()) return;

            if (kartStates.DriftTurnState != TurnState.NotTurning && kartDriftSystem.HasRequiredSpeed())
            {
                kartEngine.DriftTurn(turnValue);
                kartEngine.DriftUsingForce();
                kartDriftSystem.CheckNewTurnDirection();
            }
            else if (kartStates.DriftTurnState == TurnState.NotTurning && kartStates.TurningState != TurnState.NotTurning)
            {
                InitializeDrift(turnValue);
            }
        }

        public void Accelerate(float value)
        {
            if (kartStates.IsGrounded())
            {
                kartEngine.Accelerate(value);
            }
        }

        public void Decelerate(float value)
        {
            if (kartStates.IsGrounded() && !kartStates.IsDrifting())
            {
                kartEngine.Decelerate(value);
            }
        }

        public void Turn(float turnValue)
        {
            float newTurnSensitivity = turnValue;

            kartStates.SetTurnState(turnValue);
            kartEvents.OnTurn(turnValue);

            if (Mathf.Abs(turnValue) <= 0.9f)
            {
                newTurnSensitivity = turnValue / kartEngine.LowerTurnSensitivity;
            }

            if (kartStates.DriftTurnState != TurnState.NotTurning) return;
            if (kartStates.AccelerationState == AccelerationState.None) return;

            var direction = kartStates.AccelerationState == AccelerationState.Forward ? Vector3.up : Vector3.down;
            kartEngine.TurnUsingTorque(direction * newTurnSensitivity, turnValue);
        }

        public void DestroyKart()
        {
            PhotonNetwork.RemoveRPCs(photonView);
            PhotonNetwork.Destroy(photonView);
        }
    }
}
