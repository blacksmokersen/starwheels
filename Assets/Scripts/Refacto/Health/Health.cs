using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class Health : MonoBehaviour
    {
        [Header("Health values")]
        public int MaxHealth = 3;
        public int CurrentHealth;
        public bool IsDead = false;
        public bool IsInvincible = false;

        [Header("Events")]
        public UnityEvent<int> OnHealthLoss;
        public UnityEvent OnDeath;
        public UnityEvent<float> OnInvincibilityDuration;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        // PUBLIC

        public void LoseHealth()
        {
            CurrentHealth--;
            OnHealthLoss.Invoke(CurrentHealth);
            CheckIfIsDead();
        }

        public void ResetLives()
        {
            CurrentHealth = MaxHealth;
        }

        public void SetInvincibilityForXSeconds(float x)
        {
            StartCoroutine(InvicibilityTime(x));
        }

        // PRIVATE

        private void CheckIfIsDead()
        {
            if (CurrentHealth <= 0)
            {
                IsDead = true;
                OnDeath.Invoke();
            }
        }

        private IEnumerator InvicibilityTime(float x)
        {
            IsInvincible = true;
            OnInvincibilityDuration.Invoke(x);
            yield return new WaitForSeconds(x);
            IsInvincible = false;
        }
    }
}
