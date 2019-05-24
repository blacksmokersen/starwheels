using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Common
{
    [DisallowMultipleComponent]
    public class MouseAndGamepadNavigation : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;

        private Selectable _lastSelected;
        private bool _gamepadHasFocus = false;

        // CORE

        private void Awake()
        {
            _lastSelected = _eventSystem.firstSelectedGameObject.GetComponent<Selectable>();
        }

        private void Update()
        {
            CheckCurrentSelected();
            CheckGamepadInputs();
            CheckMouseInputs();
        }

        // PUBLIC

        public GameObject GameObjectUnderPointer(int pointerId)
        {
            /*
            var lastPointer = _eventSystem.GetLastPointerEventData(pointerId);
            if (lastPointer != null)
            {
                return lastPointer.pointerCurrentRaycast.gameObject;
            }
            */
            return null;
        }

        public GameObject GameObjectUnderPointer()
        {
            return GameObjectUnderPointer(PointerInputModule.kMouseLeftId);
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
                _eventSystem.SetSelectedGameObject(_lastSelected.gameObject);
                Cursor.visible = false;
                _gamepadHasFocus = true;
            }
        }

        private void CheckMouseInputs()
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
            {
                Cursor.visible = true;
                _gamepadHasFocus = false;
            }
        }
    }
}
