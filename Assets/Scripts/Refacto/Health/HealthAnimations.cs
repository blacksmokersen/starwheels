using UnityEngine;

namespace Health
{
    public class HealthAnimations : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            GetComponentInParent<Animator>();
        }

        public void HighSpeedHitAnimation()
        {
            _animator.SetTrigger("HitHighSpeed");
        }

        public void LowSpeedHitAnimation()
        {
            _animator.SetTrigger("HitLowSpeed");
        }
    }
}
