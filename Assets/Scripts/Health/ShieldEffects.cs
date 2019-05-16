using System.Collections;
using UnityEngine;
using Items;

namespace Health
{
    public class ShieldEffects : MonoBehaviour
    {
        [SerializeField] private Health _health;

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void ActivateForXSeconds(float x)
        {
            Activate();
            _health.SetInvincibilityForXSeconds(x);
            StartCoroutine(DeactivateAfterXSecondsRoutine(x));
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _health.UnsetInvincibility();
            StopAllCoroutines();
        }

        // PRIVATE

        private IEnumerator DeactivateAfterXSecondsRoutine(float x)
        {
            yield return new WaitForSeconds(x);
            Deactivate();
        }

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
            // TODO OVERCHARGE
        }
    }
}
