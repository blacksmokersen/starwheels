using UnityEngine;
using UnityEngine.Events;

namespace Menu
{
    [DisallowMultipleComponent]
    public class MenuOpener : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _buttonName;
        [SerializeField] private bool _toggleOnKeyDown;
        [SerializeField] private GameObject _menuPanel;

        [Header("Events")]
        public UnityEvent OnMenuOpened;
        public UnityEvent OnMenuClosed;

        // CORE

        private void Update()
        {
            if (Input.GetButtonDown(_buttonName))
            {
                if (_toggleOnKeyDown)
                {
                    _menuPanel.SetActive(!_menuPanel.activeInHierarchy);
                }
                else
                {
                    _menuPanel.SetActive(true);
                }

                // Events
                if (_menuPanel.activeInHierarchy && OnMenuOpened != null)
                {
                    OnMenuOpened.Invoke();
                }
                else if (!_menuPanel.activeInHierarchy && OnMenuClosed != null)
                {
                    OnMenuClosed.Invoke();
                }
            }
        }
    }
}
