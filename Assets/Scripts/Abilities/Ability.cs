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
        [SerializeField] protected AbilitySettings abilitySettings;

        [Header("Reload Effects")]
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Unity Events")]
        public UnityEvent OnAbilityReload;

        // MONOBEHAVIOUR

        private void OnDisable()
        {
            Reload();
        }

        private void OnEnable()
        {
            Reload();
        }

        // PUBLIC

        public void Reload()
        {
            OnAbilityReload.Invoke();
        }

        // PROTECTED

        protected IEnumerator Cooldown()
        {
            CanUseAbility = false;
            yield return new WaitForSeconds(abilitySettings.CooldownDuration);
            CanUseAbility = true;
            OnAbilityReload.Invoke();
            ReloadEffects();
        }

        protected void ReloadEffects()
        {
            _particleSystem.Emit(abilitySettings.ReloadParticleNumber);
        }
    }
}
