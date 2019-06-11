using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Common
{
    [DisallowMultipleComponent]
    public class MouseAndGamepadNavigation : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;

        [Header("Settings")]
        public bool Enabled;
        [SerializeField] private bool _disableCursorOnAwake;

        private Selectable _lastSelected;
        private bool _gamepadHasFocus = false;

        // CORE

        private void Awake()
        {
            _lastSelected = _eventSystem.firstSelectedGameObject.GetComponent<Selectable>();
            HideCursor();

            if (_disableCursorOnAwake)
            {
                HideCursor();
            }
        }

        private void Update()
        {
            if (Enabled)
            {
                CheckCurrentSelected();
                CheckGamepadInputs();
                CheckMouseInputs();
            }
        }

        // PUBLIC

        public void Enable(bool b)
        {
            Enabled = b;
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // PRIVATE

        private void CheckCurrentSelected()
        {
            if (_eventSystem.currentSelectedGameObject)
            {
                var currentSelected = _eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
                if (currentSelected != _lastSelected)
                {
                    _lastSelected = currentSelected;
                }
            }
        }

        private void CheckGamepadInputs()
        {
            if (!_gamepadHasFocus && (Mathf.Abs(Input.GetAxis(Constants.Input.TurnAxis)) > 0 || Mathf.Abs(Input.GetAxis(Constants.Input.UpAndDownAxis)) > 0))
            {
                _eventSystem.SetSelectedGameObject(null);
                _eventSystem.SetSelectedGameObject(_lastSelected.gameObject);
                HideCursor();
                _gamepadHasFocus = true;
            }
        }

        private void CheckMouseInputs()
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
            {
                _eventSystem.SetSelectedGameObject(null);
                ShowCursor();
                _gamepadHasFocus = false;
            }
        }
    }
}
