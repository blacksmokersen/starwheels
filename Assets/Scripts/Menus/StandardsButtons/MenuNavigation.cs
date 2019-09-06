using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Menu.Options
{
    public class MenuNavigation : MonoBehaviour
    {
        [SerializeField] private ButtonAttribution[] _buttons;

        public UnityEvent ValidationEvent;
        public UnityEvent[] _buttonEvents;
        [SerializeField] private UnityEvent _OnButtonChangeEvent;
        [SerializeField] private UnityEvent _B_ButtonChangeEvent;

        [SerializeField] private bool _controllerInteraction = true;
        [SerializeField] private bool _canMove = true;
        [SerializeField] private bool _horizontalMenu;

        [SerializeField] private float _triggerMoveCooldownDuration = 0.2f;
        private float _triggerMoveCooldown;

        public int MenuPosition = 0;
        private int _previousMenuPos;

        // MONO

        private void OnEnable()
        {
            if (_buttons.Length > 0)
            {
                if (_buttons[0])
                {
                    ResetMenu();
                }
            }
        }

        private void Update()
        {
            if (_controllerInteraction)
            {
                InputProgression();
            }

            if (!_canMove) // trigger move cooldown gestion
            {
                if (_triggerMoveCooldown > 0)
                {
                    _triggerMoveCooldown -= Time.deltaTime;
                }
                else
                {
                    _canMove = true;
                    _triggerMoveCooldown = _triggerMoveCooldownDuration;
                }
            }
        }

        //NAVIGATION

        public void EnableManualProgression(bool value)
        {
            _controllerInteraction = value;
        }

        private void ResetMenu()
        {
            _controllerInteraction = true;
            MenuPosition = 0;
            _buttons[0].HighlightButton(true); // highlight the first button
            _canMove = true;
            _triggerMoveCooldown = _triggerMoveCooldownDuration; // cooldown inputs initialization
        }

        private void InputProgression()
        {
            if (!_horizontalMenu)
            {
                //Keyboard arrow
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ChangeMenuPosition(1);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ChangeMenuPosition(-1);
                }
                //Joystick Left Stick && arrow
                if (_canMove)
                {
                    if (Input.GetAxis("UpAndDown") > 0 || Input.GetAxis("UpAndDownArrow") > 0)
                    {
                        ChangeMenuPosition(-1);
                        _canMove = false;
                    }
                    if (Input.GetAxis("UpAndDown") < 0 || Input.GetAxis("UpAndDownArrow") < 0)
                    {
                        ChangeMenuPosition(1);
                        _canMove = false;
                    }
                }
            }

            else
            {
                //Keyboard arrow
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ChangeMenuPosition(1);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ChangeMenuPosition(-1);
                }
                //Joystick Left Stick && arrow
                if (_canMove)
                {
                    if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("HorizontalArrows") > 0)
                    {
                        ChangeMenuPosition(1);
                        _canMove = false;
                    }
                    if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalArrows") < 0)
                    {
                        ChangeMenuPosition(-1);
                        _canMove = false;
                    }
                }
            }


            //Validation (Enter || A)
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                ButtonValidation();
            }

            //Exit (ESCAPE || B)
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                ReturnEvent();
            }
        }

        public void OnMouseActualization(int value)
        {
            _previousMenuPos = MenuPosition;
            MenuPosition = value;

            if (_previousMenuPos != MenuPosition)
            {
                _buttons[_previousMenuPos].HighlightButton(false);
            }

            if (_OnButtonChangeEvent != null)
            {
                _OnButtonChangeEvent.Invoke();
            }
        }

        private void ActualizePosition()
        {
            if (_previousMenuPos != MenuPosition)
            {
                _buttons[_previousMenuPos].HighlightButton(false);
            }
            _buttons[MenuPosition].HighlightButton(true);
        }

        private void ChangeMenuPosition(int value)
        {
            if (_OnButtonChangeEvent != null)
            {
                _OnButtonChangeEvent.Invoke();
            }
            _previousMenuPos = MenuPosition;
            MenuPosition += value;
            if (MenuPosition >= _buttons.Length)
            {
                MenuPosition = 0;
            }
            else if (MenuPosition < 0)
            {
                MenuPosition = _buttons.Length - 1;
            }
            ActualizePosition();
        }

        private void SetMenuPosition(int value)
        {
            if (_OnButtonChangeEvent != null)
            {
                _OnButtonChangeEvent.Invoke();
            }

            _previousMenuPos = MenuPosition;

            if (value >= _buttons.Length || value < 0)
            {
                MenuPosition = 0;
            }
            else
            {
                MenuPosition = value;
            }
            ActualizePosition();   
        }

        private void ButtonValidation()
        {
            if (_buttonEvents.Length > MenuPosition)
            {
                if (_buttonEvents[MenuPosition] != null)
                {
                    _buttonEvents[MenuPosition].Invoke();
                }

                if (ValidationEvent != null)
                {
                    ValidationEvent.Invoke();
                }
            }
        }

        public void ButtonAction(int IDButton)
        {
            if (_buttonEvents.Length > IDButton)
            {
                if (_buttonEvents[IDButton] != null)
                {
                    _buttonEvents[IDButton].Invoke();
                }

                if (ValidationEvent != null)
                {
                    ValidationEvent.Invoke();
                }
            }
        }

        public void EnableButtons(bool value)
        {
            _controllerInteraction = value;
            foreach (ButtonAttribution _button in _buttons)
            {
                _button.EnableButtonInteraction(value);
            }
        }

        public void ReturnEvent()
        {
            if (_B_ButtonChangeEvent != null)
            {
                _B_ButtonChangeEvent.Invoke();
            }
        }

        public void ClosePanel(bool value)
        {
            if (value == true)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
