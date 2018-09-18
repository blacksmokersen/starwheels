using System.Collections;
using UnityEngine;
using Kart;

namespace Abilities
{
    public class AbilityJump : Ability
    {
        [Header("Jump Ability")]
        public float CooldownDoubleJump = 1f;
        public float EnergyConsummedOnJump = 0.5f;

        private bool _hasDoneFirstJump = false;
        private bool _canDoubleJump = true;
        private KartEngine _kartEngine;
        private KartHub _kartHub;

        // CORE

        private new void Awake()
        {
            base.Awake();

            _kartEngine = GetComponent<KartEngine>();
            _kartHub = GetComponentInParent<KartHub>();
        }

        // PUBLIC

        public override void Use(float xAxis, float yAxis)
        {
            _kartHub.StopDrift();

            if (CanDoubleJump())
            {
                DoubleJump(xAxis, yAxis);
                _hasDoneFirstJump = false;
            }
            else
            {
                if (kartStates.IsGrounded() && _canDoubleJump)
                {
                    _hasDoneFirstJump = true;
                    StartCoroutine(StartCooldownDoubleJump());
                    _kartEngine.Jump();
                }
            }
        }

        // PRIVATE

        private void DoubleJump(float xAxis, float yAxis)
        {
            if (Mathf.Abs(xAxis) < 0.3f)
            {
                if (yAxis <= -0.2f)
                {
                    _kartEngine.DoubleJump(Vector3.back, 0.5f);
                    kartEvents.CallRPC("OnDoubleJump", Direction.Backward);
                }
                else if (yAxis >= 0.2f)
                {
                    _kartEngine.DoubleJump(Vector3.forward, 0.5f);
                    kartEvents.CallRPC("OnDoubleJump", Direction.Forward);
                }
                else
                {
                    _kartEngine.DoubleJump(Vector3.forward, 0f);
                    kartEvents.CallRPC("OnDoubleJump", Direction.Forward);
                }
            }
            else if (xAxis < -0.5f)
            {
                _kartEngine.DoubleJump(Vector3.left, 1f);
                kartEvents.CallRPC("OnDoubleJump", Direction.Left);
            }
            else if (xAxis >= 0.5f)
            {
                _kartEngine.DoubleJump(Vector3.right, 1f);
                kartEvents.CallRPC("OnDoubleJump", Direction.Right);
            }
            else
            {
                _kartEngine.DoubleJump(Vector3.forward, 0f);
                kartEvents.CallRPC("OnDoubleJump", Direction.Forward);
            }
        }

        private IEnumerator StartCooldownDoubleJump()
        {
            _canDoubleJump = false;
            yield return new WaitForSeconds(CooldownDoubleJump);
            _canDoubleJump = true;
            kartEvents.OnDoubleJumpReset();
        }

        private bool CanDoubleJump()
        {
            return _hasDoneFirstJump && !kartStates.IsGrounded();
        }
    }
}
