using UnityEngine;

namespace Drift
{
    public class DriftAnimation : MonoBehaviour
    {
        private Animator _animator;

        // CORE

        private void Awake()
        {
            _animator = GetComponentInParent<Animator>();
        }

        // PUBLIC

        public void LeftDriftAnimation()
        {
            _animator.SetBool("DriftLeft", true);
        }

        public void RightDriftAnimation()
        {
            _animator.SetBool("DriftRight", true);
        }

        public void NoDriftAnimation()
        {
            _animator.SetBool("DriftLeft", false);
            _animator.SetBool("DriftRight", false);
        }
    }
}
