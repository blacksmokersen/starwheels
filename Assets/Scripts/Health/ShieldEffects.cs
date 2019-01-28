using System.Collections;
using UnityEngine;

namespace Health
{
    public class ShieldEffects : MonoBehaviour
    {
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void ActivateForXSeconds(float x)
        {
            Activate();
            StartCoroutine(DeactivateAfterXSecondsRoutine(x));
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        // PRIVATE

        private IEnumerator DeactivateAfterXSecondsRoutine(float x)
        {
            yield return new WaitForSeconds(x);
            Deactivate();
        }
    }
}
