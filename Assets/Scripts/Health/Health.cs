using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Photon;

namespace Health
{
    public class Health : EntityBehaviour<IKartState>
    {
        [Header("Health values")]
        public int MaxHealth = 3;
        public int CurrentHealth;
        public bool IsDead = false;
        public bool CanLoseHealth = true;
        public bool IsInvincible = false;

        [Header("Events")]
        public IntEvent OnHealthLoss;
        public UnityEvent OnDeath;
        public FloatEvent OnInvincibilityDuration;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        // BOLT

        public override void ControlGained()
        {
            state.SetDynamic("Health", CurrentHealth);
            state.AddCallback("Health", UpdateCurrentHealth);
            state.AddCallback("Health", CheckIfIsDead);
        }

        // PUBLIC

        public void SetCanLoseHealth(bool b)
        {
            CanLoseHealth = b;
        }

        public void LoseHealth()
        {
            if (CanLoseHealth && !IsInvincible)
            {
                if (entity.isOwner)
                {
                    state.Health--;
                }

                OnHealthLoss.Invoke(state.Health);
            }
        }

        public void ResetLives()
        {
            CurrentHealth = MaxHealth;
        }

        public void SetInvincibilityForXSeconds(float x)
        {
            StartCoroutine(InvicibilityTime(x));
        }

        public void SetInvincibility()
        {
            Debug.Log("[INVINCIBILITY] Setting");
            IsInvincible = true;
        }

        public void UnsetInvincibility()
        {
            Debug.Log("[INVINCIBILITY] Unsetting");
            IsInvincible = false;
        }

        // PRIVATE

        private void CheckIfIsDead()
        {
            if (entity.isAttached && state.Health <= 0 && !IsDead)
            {
                IsDead = true;
                OnDeath.Invoke();
            }
        }

        private void UpdateCurrentHealth()
        {
            if (entity.isAttached)
            {
                CurrentHealth = state.Health;
            }
        }

        private IEnumerator InvicibilityTime(float x)
        {
            Debug.Log("[INVINCIBILITY] Timer");
            IsInvincible = true;
            OnInvincibilityDuration.Invoke(x);
            yield return new WaitForSeconds(x);
            IsInvincible = false;
        }
    }
}
