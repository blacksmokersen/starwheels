using UnityEngine;

namespace Health
{
    public class HealthAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

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
