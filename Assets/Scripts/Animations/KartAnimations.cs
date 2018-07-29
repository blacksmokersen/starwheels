using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    public class KartAnimations : MonoBehaviour
    {
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {

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
