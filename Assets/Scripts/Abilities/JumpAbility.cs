using System.Collections;
using UnityEngine;
using Kart;

namespace Abilities
{
    public class JumpAbility : Ability
    {
        [Header("Jump Ability")]
        public float CooldownDoubleJump;
        public float EnergyConsummedOnJump;

        private bool hasDoneFirstJump = false;
        private bool canDoubleJump = true;
        private KartEngine kartEngine;

        private new void Awake()
        {
            base.Awake();

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
                    kartEvents.OnDoubleJump(Directions.Backward);
                }
                else if (yAxis >= 0.2f)
                {
                    kartEngine.DoubleJump(Vector3.forward, 0.5f);
                    kartEvents.OnDoubleJump(Directions.Forward);
                }
                else
                {
                    kartEngine.DoubleJump(Vector3.forward, 0f);
                    kartEvents.OnDoubleJump(Directions.Forward);
                }
            }
            else if (xAxis < -0.5f)
            {
                kartEngine.DoubleJump(Vector3.left, 1f);
                kartEvents.OnDoubleJump(Directions.Left);
            }
            else if (xAxis >= 0.5f)
            {
                kartEngine.DoubleJump(Vector3.right, 1f);
                kartEvents.OnDoubleJump(Directions.Right);
            }
            else
            {
                kartEngine.DoubleJump(Vector3.forward, 0f);
                kartEvents.OnDoubleJump(Directions.Forward);
            }
        }

        private IEnumerator StartCooldownDoubleJump()
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
