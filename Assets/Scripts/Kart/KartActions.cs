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
        public KartEngine kartEngine;
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
            kartEngine = GetComponentInParent<KartEngine>();
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
                kartEngine.CompensateSlip();
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
                        kartEngine.Jump(value);
                        kartStates.AirState = AirStates.InAir;
                        hasFirstJump = true;
                    }
                    else
                    {
                        kartEngine.Jump(value);
                        kartStates.AirState = AirStates.InAir;
                    }
                }
            }
        }

        public void UseItem(float verticalValue)
        {
            Directions direction = verticalValue >= -0.3f ? Directions.Forward : Directions.Backward;
            //Debug.Log("Direction : " + verticalValue);
            kartInventory.ItemAction(direction);
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
                if (!HasDriftJump)
                {
                    kartEngine.Jump(0.3f);
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
            kartDriftSystem.StopDrift();
            HasDriftJump = false;
        }

        public void DriftTurns(float turnValue)
        {
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
                newTurnSensitivity = value / kartEngine.LowerTurnSensitivity;
            else
                newTurnSensitivity = value;

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

        public void DoubleJump(float value, float turnAxis, float upAndDownAxis)
        {
            if (Mathf.Abs(turnAxis) < 0.3f)
            {
                if (upAndDownAxis <= -0.2f)
                {
                    kartEffects.BackJumpAnimation();
                    kartEngine.DoubleJump(Vector3.back, 0.5f);
                }
                else if (upAndDownAxis >= 0.2f)
                {
                    kartEffects.FrontJumpAnimation();
                    kartEngine.DoubleJump(Vector3.forward, 0.5f);
                }
                else
                {
                    kartEngine.DoubleJump(Vector3.forward, 0f);
                }
            }
            else if (turnAxis <= -0.5f)
            {
                kartEffects.LeftJumpAnimation();
                kartEngine.DoubleJump(Vector3.left, 1f);
            }
            else if (turnAxis >= 0.5f)
            {
                kartEffects.RightJumpAnimation();
                kartEngine.DoubleJump(Vector3.right, 1f);
            }
            else
            {
                kartEngine.DoubleJump(Vector3.forward, 0f);
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