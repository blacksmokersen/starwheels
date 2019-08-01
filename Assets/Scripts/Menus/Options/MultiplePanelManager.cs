using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Menu.Options
{
    [DisallowMultipleComponent]
    public class MultiplePanelManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private List<GameObject> _panels;

        [Header("Settings")]
        [SerializeField] private bool _enableMultiplePanelsOpened;
        [SerializeField] private bool _allowAutoMapping; // change options configuration if stick is connected on start of scene


        [Header("Buttons Generation")]
        [SerializeField] private GameObject _buttonPrefab;
        private ButtonAttribution[] _buttons;

        [SerializeField] private string[] _buttonNames;
        [SerializeField] private UnityEvent[] _buttonEvents;

        [SerializeField] private float _verticalDistance = 100.0f;
        [SerializeField] private float _horizontalDecalage = 12.0f;

        [SerializeField] private Transform _menuPanel;

        [Header("References")]
        [SerializeField] private Common.MouseAndGamepadNavigation _navigation;

        [Header("Menu Interactions")]
        public int MenuPosition = 0;
        private int _previousMenuPos;
        private bool _controllerInteraction = false;
        [SerializeField] float _triggerMoveCooldownDuration = 0.2f;
        private float _triggerMoveCooldown;
        private bool _canMove = true;

        // MONO

        private void Awake()
        {
            ButtonsAttribution();
        }

        private void Start()
        {
            if (_navigation.ControllerConnected != Common.MouseAndGamepadNavigation._Controller.None) // if default controller is a stick
            {
                _controllerInteraction = true;
                SetMenuPosition(0);
            }
        }

        private void Update()
        {
            InputProgression();

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

        // PRIVATE

        private void ButtonsAttribution()
        {
            _buttons = new ButtonAttribution[_buttonNames.Length];

            for (int i = 0; i < _buttonNames.Length; i++)
            {
                _buttons[i] = Instantiate(_buttonPrefab, new Vector2(_menuPanel.position.x + i * _horizontalDecalage, _menuPanel.position.y - i * _verticalDistance), _menuPanel.rotation, _menuPanel).GetComponent<ButtonAttribution>();
                _buttons[i].Attribution(i, _buttonNames[i], this);

            }
        }

        #region manual menu progression
        private void EnableManualProgression(bool value)
        {
            _controllerInteraction = value;
        }

        private void InputProgression()
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
            //Validation (Enter || A)
            if (Input.GetKeyDown(KeyCode.Return) ||Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                ButtonValidation();
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
        }

        private void ActualizePosition()
        {
            _buttons[_previousMenuPos].HighlightButton(false);
            _buttons[MenuPosition].HighlightButton(true);
        }

        private void ChangeMenuPosition(int value)
        {
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
            }
        }
        #endregion

        // PUBLIC

        public void ButtonAction(int IDButton)
        {
            if (_buttonEvents.Length > IDButton)
            {
                if (_buttonEvents[IDButton] != null)
                {
                    _buttonEvents[IDButton].Invoke();
                }
            }
        }

        public void HideAllPanels()
        {
            foreach (var panel in _panels)
            {
                panel.SetActive(false);
            }
        }

        public void ShowPanel(int panelIndex)
        {
            if (!_enableMultiplePanelsOpened)
            {
                HideAllPanels();
            }

            if (panelIndex >= 0 && panelIndex < _panels.Count)
            {
                _panels[panelIndex].SetActive(true);
            }
        }

        public void ShowPanel(GameObject panel)
        {
            if (_panels.Contains(panel))
            {
                if (!_enableMultiplePanelsOpened)
                {
                    HideAllPanels();
                }
                panel.SetActive(true);
            }
            else
            {
                Debug.LogError("This panel is not in the panel list.");
            }
        }
    }
}
