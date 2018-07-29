using System.Collections;
using UnityEngine;
using Kart;

namespace Capacities
{
    /*
     * Jump / Double Jump Capacity
     * 
     */ 
    public class JumpCapacity : MonoBehaviour
    {
        [Header("Jump Capacity")]
        public float CooldownDoubleJump;

        [HideInInspector] public bool HasDriftJump = false;
        [HideInInspector] public bool DoubleJumpEnabled = true;
        private bool hasDoneFirstJump = false;
        private bool canDoubleJump = true;

        private KartStates kartStates;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
        }

        public void Jump(float value, float turnAxis, float accelerateAxis, float upAndDownAxis)
        {
            if (DoubleJumpEnabled && hasDoneFirstJump && kartStates.AirState == AirStates.InAir)
            {
                /*
                kartSoundsScript.PlaySecondJump();
                kartEffects.MainJumpParticles(150);
                DoubleJump(value, turnAxis, upAndDownAxis);
                */
                KartEvents.OnDoubleJump(Items.Directions.Forward);
                hasDoneFirstJump = false;
            }
            else
            {
                if (kartStates.AirState == AirStates.Grounded && canDoubleJump)
                {
                    StartCoroutine(StartCooldownDoubleJump());
                    if (DoubleJumpEnabled)
                    {
                        kartPhysics.Jump(value);
                        kartStates.AirState = AirStates.InAir;
                        hasDoneFirstJump = true;
                    }
                    else
                    {
                        kartPhysics.Jump(value);
                        kartStates.AirState = AirStates.InAir;
                    }
                }
            }
        }

        public void DoubleJump(float value, float turnAxis, float upAndDownAxis)
        {
            if (Mathf.Abs(turnAxis) < 0.3f)
            {
                if (upAndDownAxis <= -0.2f)
                {
                    kartPhysics.DoubleJump(Vector3.back, 0.5f);
                }
                else if (upAndDownAxis >= 0.2f)
                {
                    kartPhysics.DoubleJump(Vector3.forward, 0.5f);
                }
                else
                {
                    kartPhysics.DoubleJump(Vector3.forward, 0f);
                }
            }
            else if (turnAxis <= -0.5f)
            {
                kartPhysics.DoubleJump(Vector3.left, 1f);
            }
            else if (turnAxis >= 0.5f)
            {
                kartPhysics.DoubleJump(Vector3.right, 1f);
            }
            else
            {
                kartPhysics.DoubleJump(Vector3.forward, 0f);
            }
        }

        IEnumerator StartCooldownDoubleJump()
        {
            canDoubleJump = false;
            yield return new WaitForSeconds(CooldownDoubleJump);
            KartEvents.OnDoubleJumpReset();
            canDoubleJump = true;
        }
    }
}