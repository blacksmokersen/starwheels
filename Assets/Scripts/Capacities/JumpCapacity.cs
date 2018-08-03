using System.Collections;
using UnityEngine;
using Kart;

namespace Capacities
{
    /*
     * Jump / Double Jump Capacity
     * 
     */ 
    public class JumpCapacity : BaseKartComponent, ICapacity
    {
        [Header("Jump Capacity")]
        public float CooldownDoubleJump;

        [HideInInspector] public bool HasDriftJump = false;
        private bool hasDoneFirstJump = false;
        private bool canDoubleJump = true;

        private KartStates kartStates;
        private KartEngine kartEngine;

        private new void Awake()
        {
            base.Awake();
            kartStates = GetComponentInParent<KartStates>();
            kartEngine = GetComponent<KartEngine>();
        }

        public void Use(float xAxis, float yAxis)
        {
            Jump(xAxis, yAxis);
        }

        public void Jump(float xAxis, float yAxis)
        {
            if (CanDoubleJump())
            {
                DoubleJump(xAxis, yAxis);                
                hasDoneFirstJump = false;
            }
            else
            {
                if (kartStates.AirState == AirStates.Grounded && canDoubleJump)
                {
                    hasDoneFirstJump = true;
                    StartCoroutine(StartCooldownDoubleJump());
                    kartEngine.Jump();
                }
            }
        }

        public void DoubleJump(float xAxis, float yAxis)
        {
            if (Mathf.Abs(xAxis) < 0.3f)
            {
                if (yAxis <= -0.2f)
                {
                    kartEngine.DoubleJump(Vector3.back, 0.5f);
                    kartEvents.OnDoubleJump(Items.Directions.Backward);
                }
                else if (yAxis >= 0.2f)
                {
                    kartEngine.DoubleJump(Vector3.forward, 0.5f);
                    kartEvents.OnDoubleJump(Items.Directions.Forward);
                }
                else
                {
                    kartEngine.DoubleJump(Vector3.forward, 0f);
                    kartEvents.OnDoubleJump(Items.Directions.Forward);
                }
            }
            else if (xAxis <= -0.5f)
            {
                kartEngine.DoubleJump(Vector3.left, 1f);
                kartEvents.OnDoubleJump(Items.Directions.Left);
            }
            else if (xAxis >= 0.5f)
            {
                kartEngine.DoubleJump(Vector3.right, 1f);
                kartEvents.OnDoubleJump(Items.Directions.Right);
            }
            else
            {
                kartEngine.DoubleJump(Vector3.forward, 0f);
                kartEvents.OnDoubleJump(Items.Directions.Forward);
            }
        }

        IEnumerator StartCooldownDoubleJump()
        {
            canDoubleJump = false;
            yield return new WaitForSeconds(CooldownDoubleJump);
            canDoubleJump = true;
            kartEvents.OnDoubleJumpReset();
        }

        private bool CanDoubleJump()
        {
            return hasDoneFirstJump && kartStates.AirState == AirStates.InAir;
        }
    }
}