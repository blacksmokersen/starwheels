using UnityEngine;
using UnityEngine.Events;

namespace Menu
{
    [DisallowMultipleComponent]
    public class GamepadButtonCallback : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnButtonDown;
        public UnityEvent OnButtonUp;

        [Header("Information")]
        public bool ButtonPressed;

        [SerializeField] private string _buttonName;

        // CORE

        private void Update()
        {
            if (Input.GetButtonDown(_buttonName) && OnButtonDown != null)
            {
                OnButtonDown.Invoke();
            }
            else if (Input.GetButtonUp(_buttonName) && OnButtonUp != null)
            {
                OnButtonUp.Invoke();
            }
            ButtonPressed = Input.GetButton(_buttonName);
        }
    }
}
