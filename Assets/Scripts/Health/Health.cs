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

        public override void Attached()
        {
            if (entity.attachToken != null)
            {
                var roomToken = (RoomProtocolToken)entity.attachToken;
                CanLoseHealth = roomToken.Gamemode == Constants.GameModes.Battle;
            }
            else
            {
                Debug.LogError("Couldn't find the attached token to set the knockout mode.");
            }
        }

        public override void ControlGained()
        {
            state.SetDynamic("Health", CurrentHealth);
            state.AddCallback("Health", UpdateCurrentHealth);
            state.AddCallback("Health", CheckIfIsDead);
        }

        // PUBLIC

        public void LoseHealth()
        {
            if (CanLoseHealth && !IsInvincible)
            {
                if (entity.isOwner)
                {
                    state.Health--;
                }

                OnHealthLoss.Invoke(state.Health);
                //SetInvincibilityForXSeconds(3f);
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

        // PRIVATE

        private void CheckIfIsDead()
        {
            if (state.Health <= 0 && !IsDead)
            {
                IsDead = true;
                OnDeath.Invoke();
            }
        }

        private void UpdateCurrentHealth()
        {
            CurrentHealth = state.Health;
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
