using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ControllableDisabler : MonoBehaviour
    {
        [Header("Components List")]
        [SerializeField] private List<MonoBehaviour> _componentsToDisable;
        [SerializeField] private List<MonoBehaviour> _componentsNotToDisable;

        private List<IControllable> _controllables = new List<IControllable>();
        private List<IControllable> _exceptControllables = new List<IControllable>();

        // CORE

        private void Awake()
        {
            foreach(var item in _componentsToDisable)
            {
                if(item is IControllable)
                {
                    _controllables.Add((IControllable) item);
                }
            }

            foreach (var item in _componentsNotToDisable)
            {
                if (item is IControllable)
                {
                    _exceptControllables.Add((IControllable)item);
                }
            }
        }

        // PUBLIC

        public void DisableAllForXSeconds(FloatVariable seconds)
        {
            StartCoroutine(DisableAllForXSecondsRoutine(seconds.Value));
        }

        public void EnableAllAfterXSeconds(FloatVariable seconds)
        {
            StartCoroutine(DisableAllForXSecondsRoutine(seconds.Value));
        }

        public void DisableAllInChildrenForXSeconds(FloatVariable seconds)
        {
            StartCoroutine(DisableAllInChildrenForXSecondsRoutine(seconds.Value));
        }

        public void EnableAllInChildrenAfterXSeconds(FloatVariable seconds)
        {
            StartCoroutine(DisableAllInChildrenForXSecondsRoutine(seconds.Value));
        }

        public void DisableAll()
        {
            foreach (var item in _controllables)
            {
                if (!_exceptControllables.Contains(item))
                {
                    item.Enabled = false;
                }
            }
        }

        public void EnableAll()
        {
            foreach (var item in _controllables)
            {
                item.Enabled = true;
            }
        }

        public void DisableAllInChildren()
        {
            foreach (var controllable in GetComponentsInChildren<IControllable>())
            {
                if (!_exceptControllables.Contains(controllable))
                {
                    controllable.Enabled = false;
                }
            }
        }

        public void EnableAllInChildren()
        {
            foreach (var controllable in GetComponentsInChildren<IControllable>())
            {
                controllable.Enabled = true;
            }
        }

        // PRIVATE

        private IEnumerator DisableAllForXSecondsRoutine(float seconds)
        {
            DisableAll();
            yield return new WaitForSeconds(seconds);
            EnableAll();
        }

        private IEnumerator EnableAllAfterXSecondsRoutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            EnableAll();
        }

        private IEnumerator DisableAllInChildrenForXSecondsRoutine(float seconds)
        {
            DisableAllInChildren();
            yield return new WaitForSeconds(seconds);
            EnableAllInChildren();
        }

        private IEnumerator EnableAllInChildrenAfterXSecondsRoutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            EnableAllInChildren();
        }
    }
}
