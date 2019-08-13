using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Menu.Options
{
    public class ManualManipulations : MonoBehaviour
    {
        [SerializeField] private UnityEvent _rightEvent;
        [SerializeField] private UnityEvent _leftEvent;
        [SerializeField] private UnityEvent _onChangeEvent;
        private void InputProgression()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && _rightEvent != null)
            {
                _rightEvent.Invoke();

                if (_onChangeEvent != null)
                {
                    _onChangeEvent.Invoke();
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && _leftEvent != null)
            {
                _leftEvent.Invoke();

                if (_onChangeEvent != null)
                {
                    _onChangeEvent.Invoke();
                }
            }

            //Joystick arrow
            if (Input.GetAxis("HorizontalArrows") < 0 && _rightEvent != null)
            {
                _rightEvent.Invoke();

                if (_onChangeEvent != null)
                {
                    _onChangeEvent.Invoke();
                }
            }

            if (Input.GetAxis("HorizontalArrows") > 0 && _leftEvent != null)
            {
                _leftEvent.Invoke();

                if (_onChangeEvent != null)
                {
                    _onChangeEvent.Invoke();
                }
            }
        }
    }
}