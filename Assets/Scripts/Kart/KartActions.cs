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
    public class KartActions : MonoBehaviour
    {
        public KartStates kartStates;
        public KartEngine kartEngine;
        public KartDriftSystem kartDriftSystem;
        public KartMeshMovement kartMeshMovement;
        public KartInventory kartInventory;
        public KartHealthSystem kartHealthSystem;
        public ICapacity kartCapacity;

        private float driftMinSpeedActivation = 10f;
        private bool hasDoneDriftJump = false;

        void Awake()
        {
            kartStates = GetComponent<KartStates>();

            kartEngine = GetComponentInChildren<KartEngine>();
            kartDriftSystem = GetComponentInChildren<KartDriftSystem>();
            kartInventory = GetComponentInChildren<KartInventory>();
            kartMeshMovement = GetComponentInChildren<KartMeshMovement>();
            kartHealthSystem = GetComponentInChildren<KartHealthSystem>();
            kartCapacity = GetComponentInChildren<ICapacity>();
        }

        private void FixedUpdate()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                kartEngine.CompensateSlip();
              //  kartOrientation.NotDrifting();
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
                kartEngine.DriftJump(value);
                kartStates.AirState = AirStates.InAir;
            }
        }

        public void InitializeDrift(float angle)
        {
            if (kartStates.IsGrounded() && kartEngine.PlayerVelocity >= driftMinSpeedActivation)
            {
                //kartSoundsScript.PlayDriftStart();
                if (!hasDoneDriftJump)
                {
                    kartEngine.DriftJump(1);
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
            //  kartSoundsScript.PlayDriftEnd();
            kartDriftSystem.StopDrift();
            hasDoneDriftJump = false;
        }

        public void DriftTurns(float turnValue)
        {
            // kartSoundsScript.PlayDrift();
            if (!kartStates.IsGrounded()) return;

            if (kartStates.DriftTurnState != DriftTurnStates.NotDrifting && kartEngine.PlayerVelocity >= driftMinSpeedActivation)
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
            if (kartStates.AirState != AirStates.InAir && !kartEngine.Crash)
            {
                kartEngine.Accelerate(value);
            }
        }

        public void Decelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir && !kartEngine.Crash)
            {
                kartEngine.Decelerate(value);
            }
        }

        public void Turn(float value)
        {
            float newTurnSensitivity;
            if (Mathf.Abs(value) <= 0.9f)
            {
                newTurnSensitivity = value / kartEngine.LowerTurnSensitivity;
            }
            else
            {
                newTurnSensitivity = value;
            }

            if (value > 0)
            {
                kartStates.TurningState = TurningStates.Right;
            }
            else if (value < 0)
            {
                kartStates.TurningState = TurningStates.Left;
            }
            else
            {
                kartStates.TurningState = TurningStates.NotTurning;
            }

            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                if (kartStates.AccelerationState == AccelerationStates.Forward)
                {
                    kartEngine.TurnUsingTorque(Vector3.up * newTurnSensitivity, value);
                }
                else if (kartStates.AccelerationState == AccelerationStates.Back)
                {
                    kartEngine.TurnUsingTorque(Vector3.down * newTurnSensitivity, value);
                }
            }
        }

        public void KartMeshMovement(float turnAxis)
        {
            kartMeshMovement.FrontWheelsMovement(turnAxis, kartEngine.PlayerVelocity);
            kartMeshMovement.BackWheelsMovement(kartEngine.PlayerVelocity);
        }
    }
}