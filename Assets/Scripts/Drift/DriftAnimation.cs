using UnityEngine;

namespace Drift
{
    public class DriftAnimation : Bolt.EntityBehaviour<IKartState>
    {
        [SerializeField] private Animator _animator;

        // CORE

        // PUBLIC

        public void LeftDriftAnimation()
        {
            state.DriftLeft = true;
            _animator.SetBool("DriftLeft", true);
        }

        public void RightDriftAnimation()
        {
            state.DriftRight = true;
            _animator.SetBool("DriftRight", true);
        }

        public void NoDriftAnimation()
        {
            state.DriftLeft = false;
            state.DriftRight = false;
            _animator.SetBool("DriftLeft", false);
            _animator.SetBool("DriftRight", false);
        }
    }
}
