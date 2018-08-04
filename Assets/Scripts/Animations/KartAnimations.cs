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
        public void BackJumpAnimation()
        {
            animator.SetTrigger("BackJump");
        }
    }
}
