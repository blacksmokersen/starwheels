using UnityEngine;
using System.Collections;
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
        public KartPhysics kartPhysics;
        public KartOrientation kartOrientation;
        public KartStates kartStates;
        public KartDriftSystem kartDriftSystem;
        public KartEffects kartEffects;
        public KartMeshMovement kartMeshMovement;
        public KartInventory kartInventory;
        public KartSoundsScript kartSoundsScript;
        public KartHealthSystem kartHealthSystem;

        public bool HasDriftJump = false;
        public bool DoubleJumpEnabled = true;
        private bool hasFirstJump = false;
        private bool canDoubleJump = true;

        private float driftMinSpeedActivation = 10f;

        void Awake()
        {
            kartPhysics = GetComponentInParent<KartPhysics>();
            kartOrientation = GetComponentInParent<KartOrientation>();
            kartStates = GetComponentInParent<KartStates>();
            kartDriftSystem = GetComponentInParent<KartDriftSystem>();
            kartInventory = FindObjectOfType<KartInventory>();
            kartEffects = FindObjectOfType<KartEffects>();
            kartMeshMovement = FindObjectOfType<KartMeshMovement>();
            kartSoundsScript = FindObjectOfType<KartSoundsScript>();
            kartHealthSystem = GetComponentInParent<KartHealthSystem>();
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
            if (DoubleJumpEnabled && hasFirstJump == true && kartStates.AirState == AirStates.InAir)
            {
                kartSoundsScript.PlaySecondJump();
                kartEffects.MainJumpParticles(150);
                DoubleJump(value, turnAxis, upAndDownAxis);
                hasFirstJump = false;
            }
            else
            {
                if (kartStates.AirState == AirStates.Grounded && canDoubleJump)
                {
                    StartCoroutine(CdDoubleJump());
                    kartEffects.MainJumpParticles(300);
                    if (DoubleJumpEnabled)
                    {
                        kartSoundsScript.PlayFirstJump();
                        kartPhysics.Jump(value);
                        kartStates.AirState = AirStates.InAir;
                        hasFirstJump = true;
                    }
                    else
                    {
                        kartPhysics.Jump(value);
                        kartStates.AirState = AirStates.InAir;
                    }
                }
            }
        }

        public void UseItem(float verticalValue)
        {
            Directions direction = Directions.Default;
            if (verticalValue > 0.3f) direction = Directions.Forward;
            else if (verticalValue < -0.3f) direction = Directions.Backward;
            kartInventory.ItemAction(direction);
        }

        public void DriftJump(float value = 1f)
        {
            if (kartStates.AirState == AirStates.Grounded)
            {
                kartPhysics.DriftJump(value);
                kartStates.AirState = AirStates.InAir;
            }
        }

        public void InitializeDrift(float angle)
        {
            if (kartStates.IsGrounded() && kartPhysics.PlayerVelocity >= driftMinSpeedActivation)
            {
                //kartSoundsScript.PlayDriftStart();
                if (!HasDriftJump)
                {
                    kartPhysics.Jump(0.3f);
                    HasDriftJump = true;
                }
                if (angle != 0)
                {
                    kartDriftSystem.InitializeDrift(angle);
                    HasDriftJump = false;
                }
            }
        }

        public void StopDrift()
        {
            //  kartSoundsScript.PlayDriftEnd();
            kartDriftSystem.StopDrift();
            HasDriftJump = false;
        }

        public void DriftTurns(float turnValue)
        {
            // kartSoundsScript.PlayDrift();
            if (!kartStates.IsGrounded()) return;

            if (kartStates.DriftTurnState != DriftTurnStates.NotDrifting && kartPhysics.PlayerVelocity >= driftMinSpeedActivation)
            {
                kartOrientation.DriftTurn(turnValue);
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
            float newTurnSensitivity;
            if (Mathf.Abs(value) <= 0.9f)
            {
                newTurnSensitivity = value / kartOrientation.LowerTurnSensitivity;
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
                    kartPhysics.TurnUsingTorque(Vector3.up * newTurnSensitivity, value);
                }
                else if (kartStates.AccelerationState == AccelerationStates.Back)
                {
                    kartPhysics.TurnUsingTorque(Vector3.down * newTurnSensitivity, value);
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
            if (Mathf.Abs(turnAxis) < 0.3f)
            {
                if (upAndDownAxis <= -0.2f)
                {
                    kartEffects.BackJumpAnimation();
                    kartPhysics.DoubleJump(Vector3.back, 0.5f);
                }
                else if (upAndDownAxis >= 0.2f)
                {
                    kartEffects.FrontJumpAnimation();
                    kartPhysics.DoubleJump(Vector3.forward, 0.5f);
                }
                else
                {
                    kartPhysics.DoubleJump(Vector3.forward, 0f);
                }
            }
            else if (turnAxis <= -0.5f)
            {
                kartEffects.LeftJumpAnimation();
                kartPhysics.DoubleJump(Vector3.left, 1f);
            }
            else if (turnAxis >= 0.5f)
            {
                kartEffects.RightJumpAnimation();
                kartPhysics.DoubleJump(Vector3.right, 1f);
            }
            else
            {
                kartPhysics.DoubleJump(Vector3.forward, 0f);
            }
        }

        IEnumerator CdDoubleJump()
        {
            canDoubleJump = false;
            yield return new WaitForSeconds(8);
            kartEffects.ReloadJump();
            canDoubleJump = true;
        }
    }
}