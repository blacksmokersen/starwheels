using UnityEngine;
using Capacities;
using Items;

namespace Kart
{
    /* 
     * Main class to map the inputs to the different kart actions
     * The states should handled and modified within this class
     * 
     */
    public class KartHub : MonoBehaviour
    {
        [HideInInspector] public KartStates kartStates;
        [HideInInspector] public KartEngine kartEngine;
        [HideInInspector] public KartDriftSystem kartDriftSystem;
        [HideInInspector] public KartInventory kartInventory;
        [HideInInspector] public KartHealthSystem kartHealthSystem;
        [HideInInspector] public ICapacity kartCapacity;

        private KartEvents kartEvents;
        private float driftMinSpeedActivation = 10f;
        private bool hasDoneDriftJump = false;

        void Awake()
        {
            kartStates = GetComponent<KartStates>();
            kartEvents = GetComponent<KartEvents>();

            kartEngine = GetComponentInChildren<KartEngine>();
            kartDriftSystem = GetComponentInChildren<KartDriftSystem>();
            kartInventory = GetComponentInChildren<KartInventory>();
            kartHealthSystem = GetComponentInChildren<KartHealthSystem>();
            kartCapacity = GetComponentInChildren<ICapacity>();
            
        }

        private void FixedUpdate()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                kartEngine.CompensateSlip();
            }
        }

        public void UseItem(float verticalValue)
        {
            Directions direction = Directions.Default;
            if (verticalValue > 0.3f) direction = Directions.Forward;
            else if (verticalValue < -0.3f) direction = Directions.Backward;
            kartInventory.ItemAction(direction);
        }

        public void UseCapacity(float xAxis, float yAxis)
        {
            kartCapacity.Use(xAxis, yAxis);
        }

        public void DriftJump(float value = 1f)
        {
            if (kartStates.AirState == AirStates.Grounded)
            {
                kartEngine.DriftJump();
            }
        }

        public void InitializeDrift(float angle)
        {
            if (kartStates.IsGrounded() && kartEngine.playerVelocity >= driftMinSpeedActivation)
            {
                if (!hasDoneDriftJump)
                {
                    kartEngine.DriftJump();
                    hasDoneDriftJump = true;
                }
                if (angle != 0)
                {
                    kartDriftSystem.InitializeDrift(angle);
                    hasDoneDriftJump = false;
                }
            }
        }

        public void StopDrift()
        {
            kartDriftSystem.StopDrift();
            hasDoneDriftJump = false;
        }

        public void DriftTurns(float turnValue)
        {
            if (!kartStates.IsGrounded()) return;

            if (kartStates.DriftTurnState != DriftTurnStates.NotDrifting && kartEngine.playerVelocity >= driftMinSpeedActivation)
            {
                kartEngine.DriftTurn(turnValue);
                kartDriftSystem.DriftForces();
                kartDriftSystem.CheckNewTurnDirection();
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                if (kartStates.TurningState == TurningStates.Left)
                    kartStates.DriftTurnState = DriftTurnStates.DriftingLeft;

                if (kartStates.TurningState == TurningStates.Right)
                    kartStates.DriftTurnState = DriftTurnStates.DriftingRight;

                InitializeDrift(turnValue);
            }
        }

        public void Accelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir && !kartEngine.Crashed)
            {
                kartEngine.Accelerate(value);
            }
        }

        public void Decelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir && !kartEngine.Crashed)
            {
                kartEngine.Decelerate(value);
            }
        }

        public void Turn(float turnValue)
        {
            float newTurnSensitivity = turnValue;
            kartEvents.OnTurn(turnValue);
            if (Mathf.Abs(turnValue) <= 0.9f)
            {
                newTurnSensitivity = turnValue / kartEngine.LowerTurnSensitivity;
            }

            SetTurnState(turnValue);

            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                if (kartStates.AccelerationState == AccelerationStates.Forward)
                {
                    kartEngine.TurnUsingTorque(Vector3.up * newTurnSensitivity, turnValue);
                }
                else if (kartStates.AccelerationState == AccelerationStates.Back)
                {
                    kartEngine.TurnUsingTorque(Vector3.down * newTurnSensitivity, turnValue);
                }
            }
        }

        private void SetTurnState(float turnValue)
        {
            if (turnValue > 0)
            {
                kartStates.TurningState = TurningStates.Right;
            }
            else if (turnValue < 0)
            {
                kartStates.TurningState = TurningStates.Left;
            }
            else
            {
                kartStates.TurningState = TurningStates.NotTurning;
            }
        }
    }
}