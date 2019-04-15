using UnityEngine;

namespace Drift
{
    public class DriftAnimation : Bolt.EntityBehaviour<IKartState>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _animatorChar;

        public void LeftDriftAnimation()
        {
            if (entity.isAttached)
            {
                state.DriftLeft = true;
                _animator.SetBool("DriftLeft", true);
               // _animatorChar.SetBool("CharDriftLeft", true);
            }
        }

        public void RightDriftAnimation()
        {
            if (entity.isAttached)
            {
                state.DriftRight = true;
                _animator.SetBool("DriftRight", true);
              //  _animatorChar.SetBool("CharDriftRight", true);
            }
        }

        public void NoDriftAnimation()
        {
            if (entity.isAttached)
            {
                state.DriftLeft = false;
                _animator.SetBool("DriftLeft", false);
              //  _animatorChar.SetBool("CharDriftRight", false);
                state.DriftRight = false;
                _animator.SetBool("DriftRight", false);
               // _animatorChar.SetBool("CharDriftLeft", false);
            }
        }
    }
}
