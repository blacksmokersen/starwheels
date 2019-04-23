using UnityEngine;

namespace Abilities.Jump
{
    public class JumpAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        // PUBLIC

        public void FirstJumpAnimation()
        {
            _animator.SetTrigger("FirstJump");
        }

        public void OnHitGroundAnimation()
        {
            _animator.SetTrigger("HitGround");
        }

        public void DoubleJumpAnimation(Direction direction)
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

        // PRIVATE

        private void LeftJumpAnimation()
        {
            _animator.SetTrigger("LeftJump");
        }

        private void RightJumpAnimation()
        {
            _animator.SetTrigger("RightJump");
        }

        private void FrontJumpAnimation()
        {
            _animator.SetTrigger("FrontJump");
        }

        private void BackJumpAnimation()
        {
            _animator.SetTrigger("BackJump");
        }
    }
}
