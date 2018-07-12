using UnityEngine;

namespace Kart
{
    public class KartActions : MonoBehaviour
    {
        private KartPhysics kartPhysics;
        private KartOrientation kartOrientation;
        private KartStates kartStates;
        private KartDriftSystem kartDriftSystem;
        private KartEffects kartEffects;
        private KartMeshMovement kartMeshMovement;


        public bool hasJumped = false;
        public bool DoubleJumpEnabled = true;
        public bool firstJump = false;

        private float driftMinSpeedActivation = 10;

        void Awake()
        {
            kartPhysics = GetComponentInParent<KartPhysics>();
            kartOrientation = GetComponentInParent<KartOrientation>();
            kartStates = GetComponentInParent<KartStates>();
            kartDriftSystem = GetComponentInParent<KartDriftSystem>();
            kartEffects = FindObjectOfType<KartEffects>();
            kartMeshMovement = FindObjectOfType<KartMeshMovement>();
        }

        private void FixedUpdate()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.NotDrifting)
            {
                kartPhysics.CompensateSlip();
                kartOrientation.NotDrifting();
            }
        }

        public void Jump(float value, float turnAxis, float accelerateAxis, float upAndDownAxis)
        {
            if (DoubleJumpEnabled && firstJump == true && kartStates.AirState == AirStates.InAir)
            {
                DoubleJump(value, turnAxis, upAndDownAxis);
                firstJump = false;
            }
            else
            {
                if (kartStates.AirState == AirStates.Grounded)
                {
                    if (DoubleJumpEnabled)
                    {
                        kartPhysics.Jump(value);
                        kartStates.AirState = AirStates.InAir;
                        firstJump = true;
                    }
                    else
                    {
                        kartPhysics.Jump(value);
                        kartStates.AirState = AirStates.InAir;
                    }
                }
            }
        }

        public void Fire()
        {
            Debug.Log("Firing");
        }

        public void InitializeDrift(float angle)
        {
            if (kartStates.AirState == AirStates.Grounded && kartPhysics.PlayerVelocity >= driftMinSpeedActivation)
            {
                if (!hasJumped)
                {
                    kartPhysics.Jump(0.3f);
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
            hasJumped = false;
        }

        public void DriftTurns(float turnValue)
        {
            if (kartStates.AirState == AirStates.InAir) return;

            if (kartStates.DriftTurnState != DriftTurnStates.NotDrifting && kartPhysics.PlayerVelocity >= driftMinSpeedActivation)
            {
                kartOrientation.DriftTurn(turnValue);
                kartDriftSystem.DriftForces();
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
            if (kartStates.AirState != AirStates.InAir && !kartOrientation.Crash)
            {
                kartPhysics.Accelerate(value);
            }
        }

        public void Decelerate(float value)
        {
            if (kartStates.AirState != AirStates.InAir && !kartOrientation.Crash)
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
                else if (kartStates.AccelerationState == AccelerationStates.Back)
                {
                    kartPhysics.TurnUsingTorque(Vector3.down * value);
                }
            }
        }

        public void KartMeshMovement(float turnAxis)
        {
            kartMeshMovement.FrontWheelsMovement(turnAxis, kartPhysics.PlayerVelocity);
            kartMeshMovement.BackWheelsMovement(kartPhysics.PlayerVelocity);
        }

        public void DoubleJump(float value, float turnAxis, float upAndDownAxis)
        {
            // optimiser les methodes sur  karphysics avec un argument Vector3
            if (Mathf.Abs(turnAxis) < 0.3f)
            {
                if (upAndDownAxis >= 0.2f)
                {
                    kartEffects.BackJumpAnimation();
                    kartPhysics.BackJump(value);
                }
                else if (upAndDownAxis <= -0.2f)
                {
                    kartEffects.FrontJumpAnimation();
                    kartPhysics.FrontJump(value);
                }
                else
                {
                    kartPhysics.StraightJump(value);
                }
            }
            else if (turnAxis <= -0.5f)
            {
                kartEffects.LeftJumpAnimation();
                kartPhysics.LeftJump(value);
            }
            else if (turnAxis >= 0.5f)
            {
                kartEffects.RightJumpAnimation();
                kartPhysics.RightJump(value);
            }
            else
            {
                kartPhysics.StraightJump(value);
            }
        }
    }
}