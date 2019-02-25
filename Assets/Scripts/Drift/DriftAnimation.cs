using UnityEngine;

namespace Drift
{
    public class DriftAnimation : Bolt.EntityBehaviour<IKartState>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _animatorChar;

        public void LeftDriftAnimation()
        {
            state.DriftLeft = true;
            _animator.SetBool("DriftLeft", true);
            _animatorChar.SetBool("CharDriftLeft", true);
        }

        public void RightDriftAnimation()
        {
            state.DriftRight = true;
            _animator.SetBool("DriftRight", true);
            _animatorChar.SetBool("CharDriftRight", true);
        }

        public void NoDriftAnimation()
        {
            state.DriftLeft = false;
            _animator.SetBool("DriftLeft", false);
            _animatorChar.SetBool("CharDriftRight", false);
            state.DriftRight = false;
            _animator.SetBool("DriftRight", false);
            _animatorChar.SetBool("CharDriftLeft", false);
        }
    }
}
