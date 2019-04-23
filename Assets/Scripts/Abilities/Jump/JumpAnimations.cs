using UnityEngine;

namespace Abilities.Jump
{
    public class JumpAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _charAnimator;

        // PUBLIC

        public void FirstJumpAnimation()
        {
            _animator.SetTrigger("FirstJump");
        }

        public void OnHitGroundAnimation()
        {
            _animator.SetTrigger("HitGround");
            _charAnimator.SetTrigger("HitGround");
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
          //  _charAnimator.SetTrigger("LeftJump");
        }

        private void RightJumpAnimation()
        {
            _animator.SetTrigger("RightJump");
           // _charAnimator.SetTrigger("RightJump");
        }

        private void FrontJumpAnimation()
        {
            _animator.SetTrigger("FrontJump");
          //  _charAnimator.SetTrigger("FrontJump");
        }

        private void BackJumpAnimation()
        {
            _animator.SetTrigger("BackJump");
           // _charAnimator.SetTrigger("BackJump");
        }
    }
}
