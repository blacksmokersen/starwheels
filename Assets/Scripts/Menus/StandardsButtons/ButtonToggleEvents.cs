using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Menu.Options
{
    public class ButtonToggleEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent  _falseEvent;
        [SerializeField] private UnityEvent  _trueEvent;

        public void ToggleEvent(bool value)
        {
            if (value)
            {
                if (_trueEvent != null)
                {
                    _trueEvent.Invoke();
                }
            }
            else
            {
                if (_falseEvent != null)
                {
                    _falseEvent.Invoke();
                }
            }
        }
    }
}
