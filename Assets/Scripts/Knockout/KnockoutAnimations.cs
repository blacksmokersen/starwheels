using UnityEngine;

namespace Knockout
{
    public class KnockoutAnimations : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private Animator _animator;

        public void PlayKnockoutAnimation()
        {
            Debug.Log("Playing anim");
            _animator.SetTrigger(Constants.Animations.Knockout);
        }
    }
}
