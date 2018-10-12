using UnityEngine;

namespace Drift
{
    public class DriftAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        // CORE

        // PUBLIC

        public void LeftDriftAnimation()
        {
            Debug.Log("Left driftuuu");
            Debug.LogFormat("Animator is null : {0}", (_animator == null));
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
