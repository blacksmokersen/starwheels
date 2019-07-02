using UnityEngine;

namespace Knockout
{
    public class KnockoutAnimations : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _charAnimator;

        public void PlayKnockoutAnimation()
        {
            Debug.Log("Playing anim");
            _animator.SetTrigger(Constants.Animations.Knockout);
            _animator.SetTrigger(("HitBack"));
            _charAnimator.SetTrigger(Constants.Animations.Knockout);
            _charAnimator.SetTrigger(("HitBack"));
        }
    }
}
