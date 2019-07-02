using UnityEngine;

namespace Abilities
{
    public class AbilityCooldownAnimationsEventsManager : MonoBehaviour, IObserver
    {
        private AbilitySetter _abilitySetter;

        //PUBLIC

        public void Observe(GameObject kartRoot)
        {
            _abilitySetter = kartRoot.GetComponentInChildren<AbilitySetter>();
        }

        public void TriggerCooldownResetAnimation(float cooldownAnimationDuration)
        {
            var animator = GetComponent<Animator>();
            animator.SetTrigger("StartCooldown");
            animator.SetFloat("SpeedAnimMult", 1 / cooldownAnimationDuration);
        }

        public void OnStartCooldownAnimation()
        {

        }

        public void OnCooldownCompleteAnimation()
        {
            if (_abilitySetter)
            {
                _abilitySetter.GetCurrentAbility().OnCooldownCompleteAnimation();
            }
        }
    }

}
