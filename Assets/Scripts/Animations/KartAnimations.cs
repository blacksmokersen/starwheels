using UnityEngine;
using Kart;
using Photon.Pun;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class KartAnimations : BaseKartComponent
    {
        private Animator animator;

        // CORE

        private new void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();

            kartEvents.OnDoubleJump += DoubleJumpAnimation;
            kartEvents.OnHealthLoss += (a) => PlayerHitAnimation();
            kartEvents.OnDriftLeft += LeftDriftAnimation;
            kartEvents.OnDriftRight += RightDriftAnimation;
            kartEvents.OnDriftEnd += NoDriftAnimation;
            kartEvents.OnDriftBoostStart += NoDriftAnimation;
        }

        // PUBLIC

        // PRIVATE

        private void DoubleJumpAnimation(Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    FrontJumpAnimation();
                    break;
                case Direction.Backward:
                    BackJumpAnimation();
                    break;
                case Direction.Left:
                    LeftJumpAnimation();
                    break;
                case Direction.Right:
                    RightJumpAnimation();
                    break;
            }
        }

        private void LeftJumpAnimation()
        {
            animator.SetTrigger("LeftJump");
        }

        private void RightJumpAnimation()
        {
            animator.SetTrigger("RightJump");
        }

        private void FrontJumpAnimation()
        {
            animator.SetTrigger("FrontJump");
        }

        private void LeftDriftAnimation()
        {
            animator.SetBool("DriftLeft", true);
        }

        private void RightDriftAnimation()
        {
            animator.SetBool("DriftRight", true);
        }

        private void NoDriftAnimation()
        {
            animator.SetBool("DriftLeft", false);
            animator.SetBool("DriftRight", false);
        }
        private void BackJumpAnimation()
        {
            animator.SetTrigger("BackJump");
        }

        private void PlayerHitAnimation()
        {
            if (kartHub.kartEngine.PlayerVelocity >= 10)
            {
                animator.SetTrigger("HitHighSpeed");
            }
            else
            {
                animator.SetTrigger("HitLowSpeed");
            }
        }
    }
}
