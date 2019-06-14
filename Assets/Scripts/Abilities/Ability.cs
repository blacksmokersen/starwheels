using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Multiplayer;

namespace Abilities
{
    public class Ability : EntityBehaviour<IKartState>
    {
        [Header("Settings")]
        public bool CanUseAbility = true;
        [SerializeField] protected AbilitySettings abilitySettings;
        [SerializeField] protected PlayerSettings _playerSettings;

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

        public void OnResetCoolDownAnimation(float cooldownSeconds)
        {
            CanUseAbility = false;
            AbilityCooldownAnimationsEventsManager cooldownAnimationsEventsManager = FindObjectOfType<AbilityCooldownAnimationsEventsManager>();
            cooldownAnimationsEventsManager.TriggerCooldownResetAnimation(cooldownSeconds);
        }

        public void OnCooldownCompleteAnimation()
        {
            CanUseAbility = true;
            OnAbilityReload.Invoke();
        }

        // PROTECTED

        protected IEnumerator CooldownRoutine()
        {
            var cooldownSeconds = abilitySettings.CooldownDuration;
            if (_playerSettings.KartIndex == _playerSettings.AbilityIndex)
            {
                cooldownSeconds -= 4f;
                Debug.Log("[ABILITY] CD Reduced.");
            }
            OnResetCoolDownAnimation(cooldownSeconds);
            yield return new WaitForSeconds(0.1f);
        }

        protected void ReloadEffects()
        {
            _particleSystem.Emit(abilitySettings.ReloadParticleNumber);
        }
    }
}
