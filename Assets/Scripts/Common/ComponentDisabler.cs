using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common
{
    public class ComponentDisabler : MonoBehaviour
    {
        public MonoBehaviour Component;

        [Header("Optional")]
        [SerializeField] private float _disabledSeconds = 0f;

        // PUBLIC

        public void Disable()
        {
            Component.enabled = false;
        }

        public void Enable()
        {
            Component.enabled = true;
        }

        public void DisableForXSeconds()
        {
            Assert.AreNotEqual(_disabledSeconds,0f, "Component not properly initialized");
            StartCoroutine(DisableForXSecondsRoutine(_disabledSeconds));
        }

        public void DisableForXSeconds(float x)
        {
            StartCoroutine(DisableForXSecondsRoutine(x));
        }

        // PRIVATE

        private IEnumerator DisableForXSecondsRoutine(float x)
        {
            Disable();
            yield return new WaitForSeconds(x);
            Enable();
        }
    }
}
