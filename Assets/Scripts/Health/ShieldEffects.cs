using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Items;

namespace Health
{
    [DisallowMultipleComponent]
    public class ShieldEffects : MonoBehaviour
    {
        public bool CanBeUsed = true;

        [Header("References")]
        [SerializeField] private Health _health;
        [SerializeField] private GameObject _shieldGraphics;

        [Header("Settings")]
        [SerializeField] private float _secondsShieldActivated;
        [SerializeField] private float _cooldownSeconds;

        [Header("Events")]
        public UnityEvent OnShieldBreak;
        public FloatEvent OnCooldownUpdated;

        private bool _cooldownStarted = false;

        // MONO

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsServer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.ItemCollisionHitBox)) // It is an item collision
                {
                    var itemCollisionTrigger = other.GetComponent<ItemCollisionTrigger>();

                    if (itemCollisionTrigger.ItemCollision.HitsPlayer) // It is an item that damages the player
                    {
                        StartCoroutine(DeactivateAfterXSecondsRoutine(.1f));
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            // USE FOR OVERCHARGE
        }

        // PUBLIC

        public void Activate()
        {
            _shieldGraphics.SetActive(true);
            _health.SetInvincibilityForXSeconds(_secondsShieldActivated);

            StartCoroutine(DeactivateAfterXSecondsRoutine(_secondsShieldActivated));
            if (!_cooldownStarted)
            {
                StartCoroutine(CooldownRoutine());
            }
        }

        public void Deactivate()
        {
            _shieldGraphics.SetActive(false);
            _health.UnsetInvincibility();
        }

        // PRIVATE

        private IEnumerator DeactivateAfterXSecondsRoutine(float x)
        {
            yield return new WaitForSeconds(x);
            if (_shieldGraphics.activeSelf)
                OnShieldBreak.Invoke();
            Deactivate();
        }

        private IEnumerator CooldownRoutine()
        {
            CanBeUsed = false;
            _cooldownStarted = true;

            var secondsElapsed = 0f;
            while (secondsElapsed < _cooldownSeconds)
            {
                if (OnCooldownUpdated != null)
                {
                    OnCooldownUpdated.Invoke(secondsElapsed / _cooldownSeconds);
                }

                yield return new WaitForSeconds(1f);
                secondsElapsed += 1f;
            }
            OnCooldownUpdated.Invoke(1f);

            _cooldownStarted = false;
            CanBeUsed = true;
        }
    }
}
