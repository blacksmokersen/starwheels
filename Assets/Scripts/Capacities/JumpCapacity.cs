using System.Collections;
using UnityEngine;
using Kart;

namespace Capacities
{
    /*
     * Jump / Double Jump Capacity
     *
     */
    public class JumpCapacity : Capacity
    {
        [Header("Jump Capacity")]
        public float CooldownDoubleJump;
        public float EnergyConsummedOnJump;

        private bool hasDoneFirstJump = false;
        private bool canDoubleJump = true;
        private KartEngine kartEngine;

        private new void Awake()
        {
            base.Awake();
            kartStates = GetComponentInParent<KartStates>();
            kartEngine = GetComponent<KartEngine>();
        }

        public override void Use(float xAxis, float yAxis)
        {
            Jump(xAxis, yAxis);
            /*
            Debug.Log("Hello");
            if (Energy >= EnergyConsummedOnJump)
            {
                Debug.Log("Friend");
                Jump(xAxis, yAxis);
                Energy -= 0.5f;
                kartEvents.OnEnergyConsumption(Energy);
            }
            */
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
                if (kartStates.AirState == AirState.Ground && canDoubleJump)
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
                    kartEvents.OnDoubleJump(Direction.Backward);
                }
                else if (yAxis >= 0.2f)
                {
                    kartEngine.DoubleJump(Vector3.forward, 0.5f);
                    kartEvents.OnDoubleJump(Direction.Forward);
                }
                else
                {
                    kartEngine.DoubleJump(Vector3.forward, 0f);
                    kartEvents.OnDoubleJump(Direction.Forward);
                }
            }
            else if (xAxis < -0.5f)
            {
                kartEngine.DoubleJump(Vector3.left, 1f);
                kartEvents.OnDoubleJump(Direction.Left);
            }
            else if (xAxis >= 0.5f)
            {
                kartEngine.DoubleJump(Vector3.right, 1f);
                kartEvents.OnDoubleJump(Direction.Right);
            }
            else
            {
                kartEngine.DoubleJump(Vector3.forward, 0f);
                kartEvents.OnDoubleJump(Direction.Forward);
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
            return hasDoneFirstJump && kartStates.AirState == AirState.Air;
        }
    }
}
