using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Menu
{
    [System.Serializable]
    public class MenuElementOption
    {
        public GameObject Element;
        public bool Activated;
    }

    [DisallowMultipleComponent]
    public class InteractibleMenu : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnElementsReset;
        public UnityEvent OnFocusSet;

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
        }

        // PUBLIC

        public void SetFocus()
        {
            if (_firstItemSelected)
            {
                _firstItemSelected.Select();
                OnFocusSet.Invoke();
            }
            else
            {
                Debug.LogWarning("SetFocus() : No item to select.");
            }
        }

        public void ResetElements()
        {
            Debug.Log("Resetting menu elements.");
            for (int i = 0; i < _elementsToReset.Count; i++)
            {
                _elementsToReset[i].SetActive(_elementsToResetStatuses[i]);// _elementsToResetStatuses[i]);
            }
            OnElementsReset.Invoke();
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
