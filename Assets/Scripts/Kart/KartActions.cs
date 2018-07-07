using UnityEngine;

namespace Kart
{
    public class KartActions : MonoBehaviour
    {
        private KartPhysics kartPhysics;
        private KartOrientation kartOrientation;
        private KartStates kartStates;
        private KartDriftSystem kartDriftSystem;

        private bool hasJumped = false;

        void Awake()
        {
            kartPhysics = GetComponentInParent<KartPhysics>();
            kartOrientation = GetComponentInParent<KartOrientation>();
            kartStates = GetComponentInParent<KartStates>();
            kartDriftSystem = GetComponentInParent<KartDriftSystem>();
        }

        private void FixedUpdate()
        {
            if(kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                kartPhysics.CompensateSlip();
            }
        }

        public void Jump(float value = 1f)
        {
            if (kartStates.AirState == AirStates.Grounded)
            {
                kartPhysics.Jump(value);
                kartStates.AirState = AirStates.InAir;
            }
        }

        public void Fire()
        {
            Debug.Log("Firing");
        }

        public void InitializeDrift(float angle)
        {
            if (kartStates.AirState == AirStates.Grounded)
            {
                if (!hasJumped)
                {
                    Jump(0.5f);
                    hasJumped = true;
                }
                if (angle != 0)
                {
                    kartDriftSystem.InitializeDrift(angle);
                    hasJumped = false;
                }
            }
        }

        public void StopDrift()
        {
            kartDriftSystem.StopDrift();
        }

        public void DriftTurns(float turnValue)
        {
            if (kartStates.AirState == AirStates.InAir) return;
            
            if (kartStates.DriftTurnState != DriftTurnStates.NotDrifting)
            {
                kartOrientation.DriftTurn(turnValue);
                kartDriftSystem.DriftForces(turnValue);
                kartDriftSystem.CheckNewTurnDirection();                
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                if (kartStates.TurningState == TurningStates.Left)
                {
                    kartStates.DriftTurnState = DriftTurnStates.DriftingLeft;
                }
                if (kartStates.TurningState == TurningStates.Right)
                {
                    kartStates.DriftTurnState = DriftTurnStates.DriftingRight;
                }
                InitializeDrift(turnValue);
            }            
        }

        public void Accelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir)
            {
                kartPhysics.Accelerate(value);
            }
        }

        public void Decelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir)
            {
                kartPhysics.Decelerate(value);
            }
        }

        public void Turn(float value)
        {
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
                    kartPhysics.TurnUsingTorque(Vector3.up * value);
                }
                else if(kartStates.AccelerationState == AccelerationStates.Back)
                {
                    kartPhysics.TurnUsingTorque(Vector3.down * value);
                }
            }
        }
    }
}