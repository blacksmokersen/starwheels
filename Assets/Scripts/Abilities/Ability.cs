using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Abilities
{
    public class Ability : EntityBehaviour<IKartState>
    {
        [Header("Ability Settings")]
        public bool CanUseAbility = true;
        [SerializeField] private AbilitySettings _abilitySettings;

        [Header("Reload Effects")]
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Unity Events")]
        public UnityEvent OnAbilityReload;

        protected IEnumerator Cooldown()
        {
            CanUseAbility = false;
            yield return new WaitForSeconds(_abilitySettings.CooldownDuration);
            CanUseAbility = true;
            OnAbilityReload.Invoke();
            ReloadEffects();
        }

        protected void ReloadEffects()
        {
            _particleSystem.Emit(_abilitySettings.ReloadParticleNumber);
        }
    }
}
