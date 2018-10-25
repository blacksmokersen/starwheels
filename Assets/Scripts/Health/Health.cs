using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Health
{
    public class Health : EntityBehaviour<IKartState>
    {
        [Header("Health values")]
        public int MaxHealth = 3;
        public int CurrentHealth;
        public bool IsDead = false;
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
            state.AddCallback("Health", CheckIfIsDead);
        }

        // PUBLIC

        public void LoseHealth()
        {
            if (!IsInvincible)
            {
                if (entity.isOwner)
                    state.Health--;

                OnHealthLoss.Invoke(state.Health);
                CheckIfIsDead();
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
            if (state.Health <= 0)
            {
                if (entity.isOwner)
                {
                    KartDestroyed kartDestroyedEvent = KartDestroyed.Create();
                    kartDestroyedEvent.Team = Multiplayer.Teams.TeamsColors.GetColorFromTeam(Multiplayer.PlayerSettings.Me.Team);
                }
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
