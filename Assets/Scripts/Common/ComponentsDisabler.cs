using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common
{
    public class ComponentsDisabler : MonoBehaviour
    {
        public List<MonoBehaviour> TargetComponents;

        [Header("Optional")]
        [SerializeField] private float _disabledSeconds = 0f;
        [SerializeField] private bool _onlyConsiderEnabledAtStart = true;

        private void Start()
        {
            Assert.IsNotNull(TargetComponents, "There are no component to disable. Check the reference.");
            if (_onlyConsiderEnabledAtStart)
            {
                for (var i = TargetComponents.Count - 1; i >= 0; i--)
                {
                    var component = TargetComponents[i];
                    if (component.gameObject.activeInHierarchy == false)
                    {
                        TargetComponents.Remove(component);
                    }
                }
            }
        }

        // PUBLIC

        public void Disable()
        {
            Assert.IsNotNull(TargetComponents, "There are no component to disable. Check the reference.");
            foreach (var component in TargetComponents)
            {
                component.enabled = false;
            }
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

        public void Enable()
        {
            Assert.IsNotNull(TargetComponents, "There are no component to enable. Check the reference.");
            foreach (var component in TargetComponents)
            {
                component.enabled = true;
            }
        }

        public void EnableAfterXSeconds()
        {
            Assert.AreNotEqual(_disabledSeconds, 0f, "Component not properly initialized");
            StartCoroutine(EnableAfterXSecondsRoutine(_disabledSeconds));
        }

        public void EnableAfterXSeconds(float x)
        {
            StartCoroutine(EnableAfterXSecondsRoutine(x));
        }

        // PRIVATE

        private IEnumerator DisableForXSecondsRoutine(float x)
        {
            Disable();
            yield return new WaitForSeconds(x);
            Enable();
        }

        private IEnumerator EnableAfterXSecondsRoutine(float x)
        {
            yield return new WaitForSeconds(x);
            Enable();
        }
    }
}
