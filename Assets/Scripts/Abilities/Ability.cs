using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class Ability : BaseKartComponent
    {
        [Header("Generic")]
        [SerializeField] protected float cooldownSeconds;

        protected bool canUseAbility = true;

        // CORE

        protected new void Awake()
        {
            base.Awake();

            cooldownSeconds = 5f;
        }

        // PUBLIC

        public virtual void Use(float xAxis, float yAxis)
        {
            // To implement in concrete abilities
        }

        // PROTECTED

        protected IEnumerator AbilityCooldown()
        {
            canUseAbility = false;
            yield return new WaitForSeconds(cooldownSeconds);
            canUseAbility = true;
            kartEvents.OnAbilityReset();
        }

        // PRIVATE
    }
}
