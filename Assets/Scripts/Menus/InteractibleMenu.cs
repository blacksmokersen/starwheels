using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Menu
{
    [DisallowMultipleComponent]
    public class InteractibleMenu : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnEnabled;
        public UnityEvent OnDisabled;

        [Header("Settings")]
        [SerializeField] private Selectable _firstItemSelected;
        [SerializeField] private bool _setFocusOnStart;
        [SerializeField] private bool _setFocusOnEnabled;
        [SerializeField] private bool _resetOnEnabled;

        [SerializeField] private List<GameObject> _elementsToReset;
        [SerializeField] private List<bool> _elementsToResetStatuses;

        // MONO

        private void Start()
        {
            if (_setFocusOnStart)
            {
                SetFocus();
            }
        }

        private void OnEnable()
        {
            if (_resetOnEnabled)
            {
                ResetElements();
            }
            if (_setFocusOnEnabled)
            {
                SetFocus();
            }

            if (OnEnabled != null)
            {
                OnEnabled.Invoke();
            }
        }

        private void OnDisable()
        {
            if (OnDisabled != null)
            {
                OnDisabled.Invoke();
            }
        }

        // PUBLIC

        public void SetFocus()
        {
            if (_firstItemSelected)
            {
                _firstItemSelected.Select();
            }
            else
            {
                Debug.LogWarning("SetFocus() : No item to select.");
            }
        }

        public void ResetElements()
        {
            for (int i = 0; i < _elementsToReset.Count; i++)
            {
                _elementsToReset[i].SetActive(_elementsToResetStatuses[i]);
            }
        }

        public void ShowAndResetElements()
        {
            gameObject.SetActive(true);
            ResetElements();
        }

        public void ResetElementsAndHide()
        {
            ResetElements();
            gameObject.SetActive(false);
        }
    }
}
