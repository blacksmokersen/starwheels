using UnityEngine;
using System.Collections;

namespace Kart
{
    public class KartHealthSystem : BaseKartComponent
    {
        public int MaxHealth = 3;
        public int Health;
        public float CrashInvincibilityDuration = 3f;
        public bool IsInvincible = false;
        public bool IsDead = false;

        // CORE

        private new void Awake()
        {
            base.Awake();

            ResetLives();

            kartEvents.OnHit += HealthLoss;
            kartEvents.OnHealthLoss += (a) => StartCoroutine(InvicibilityTime());
        }

        // PUBLIC

        public void HealthLoss()
        {
            if (!IsInvincible)
            {
                kartEvents.OnHealthLoss(--Health);
            }
            if (Health <= 0 && !IsDead)
            {
                IsDead = true;
                kartEvents.OnKartDestroyed();
            }
        }

        public void ResetLives()
        {
            Health = MaxHealth;
        }

        // PRIVATE

        private IEnumerator InvicibilityTime()
        {
            IsInvincible = true;
            yield return new WaitForSeconds(CrashInvincibilityDuration);
            IsInvincible = false;
        }
    }
}
