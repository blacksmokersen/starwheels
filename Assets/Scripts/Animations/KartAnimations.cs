using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class KartAnimations : BaseKartComponent
    {
        private Animator animator;

        private new void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();

            kartEvents.OnDoubleJump += DoubleJumpAnimation;
            kartEvents.OnHealthLoss += (a) => PlayerHitAnimation();
            kartEvents.OnDriftLeft += LeftDriftAnimation;
            kartEvents.OnDriftRight += RightDriftAnimation;
            kartEvents.OnDriftEnd += NoDriftAnimation;
            kartEvents.OnDriftBoost += NoDriftAnimation;
        }

        public void DoubleJumpAnimation(Directions direction)
        {
            switch (direction)
            {
                case Directions.Forward:
                    FrontJumpAnimation();
                    break;
                case Directions.Backward:
                    BackJumpAnimation();
                    break;
                case Directions.Left:
                    LeftJumpAnimation();
                    break;
                case Directions.Right:
                    RightJumpAnimation();
                    break;
            }
        }

        public void LeftJumpAnimation()
        {
            animator.SetTrigger("LeftJump");
        }
        public void RightJumpAnimation()
        {
            animator.SetTrigger("RightJump");
        }
        public void FrontJumpAnimation()
        {
            animator.SetTrigger("FrontJump");
        }
        public void LeftDriftAnimation()
        {
            animator.SetBool("DriftLeft", true);
        }
        public void RightDriftAnimation()
        {
            animator.SetBool("DriftRight", true);
        }
        public void NoDriftAnimation()
        {
            animator.SetBool("DriftLeft", false);
            animator.SetBool("DriftRight", false);
        }
        public void BackJumpAnimation()
        {
            animator.SetTrigger("BackJump");
        }
        public void PlayerHitAnimation()
        {
            if (kartHub.kartEngine.PlayerVelocity >= 10)
                animator.SetTrigger("HitHighSpeed");
            else
                animator.SetTrigger("HitLowSpeed");
        }
    }
}
