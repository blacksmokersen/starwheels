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
        [SerializeField] private bool _autoDisableCursorIFStickConnected;
        [SerializeField] private bool _disableCursorOnAwake;

        private Selectable _lastSelected;
        private bool _gamepadHasFocus = false;

        [SerializeField]
        public enum _Controller { None, Xbox, PS4};
        public _Controller ControllerConnected = _Controller.None;

        // CORE

        private void Awake()
        {
            DetectStickController();

            _lastSelected = _eventSystem.firstSelectedGameObject.GetComponent<Selectable>();

            if (_disableCursorOnAwake || (_autoDisableCursorIFStickConnected && ControllerConnected != _Controller.None))
            {
                HideCursor();
            }
        }

        public void DetectStickController()
        {
            string[] names = Input.GetJoystickNames();
            for (int x = 0; x < names.Length; x++)
            {
                if (names[x].Length == 19)
                {

                    ControllerConnected = _Controller.PS4;
                }
                if (names[x].Length == 33)
                {
                    ControllerConnected = _Controller.Xbox;
                }
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
                if (Cursor.visible)
                {
                    HideCursor();
                }
                _gamepadHasFocus = true;
            }
        }

        private void CheckMouseInputs()
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
            {
                //_eventSystem.SetSelectedGameObject(null);

                if (!Cursor.visible)
                {
                    ShowCursor();
                }
                if (_gamepadHasFocus)
                {
                    _gamepadHasFocus = false;
                }
            }
        }
    }
}
