using UnityEngine;

namespace Menu
{
    [DisallowMultipleComponent]
    public class RotateObjectWithMouse : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private GameObject _targetObject;

        [Header("Settings")]
        [SerializeField] private float _manualSpeed = 1f;
        [SerializeField] private float _automaticSpeed = 1f;

        private bool _canRotateObject = false;

        // CORE

        private void Update()
        {
            SlowlyRotateObject();
            CheckMouseInputs();
            CheckJoystickInputs();
        }

        // PRIVATE

        private void CheckMouseInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), Input.mousePosition, null))
                {
                    _canRotateObject = true;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_canRotateObject)
                {
                    _targetObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * _manualSpeed);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _canRotateObject = false;
            }
        }

        private void CheckJoystickInputs()
        {
            _targetObject.transform.Rotate(new Vector3(0, Input.GetAxis(Constants.Input.TurnCamera), 0) * Time.deltaTime * _manualSpeed);
        }

        private void SlowlyRotateObject()
        {
            _targetObject.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * _automaticSpeed);
        }
    }
}
