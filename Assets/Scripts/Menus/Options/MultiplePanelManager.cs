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


        [Header("Buttons Generation")]
        [SerializeField] private GameObject _buttonPrefab;
        private ButtonAttribution[] _buttons;

        [SerializeField] private string[] _buttonNames;
        [SerializeField] private UnityEvent[] _buttonEvents;

        [SerializeField] private float _verticalDistance = 100.0f;
        [SerializeField] private float _HorizontalDecalage = 12.0f;

        [SerializeField] private Transform _menuPanel;

        // MONO

        private void Awake()
        {
            ButtonsAttribution();
        }



        // PRIVATE

        private void ButtonsAttribution()
        {
            _buttons = new ButtonAttribution[_buttonNames.Length];

            for (int i = 0; i < _buttonNames.Length; i++)
            {
                _buttons[i] = Instantiate(_buttonPrefab, new Vector2(_menuPanel.position.x + i * _HorizontalDecalage, _menuPanel.position.y - i * _verticalDistance), _menuPanel.rotation, _menuPanel).GetComponent<ButtonAttribution>();
                _buttons[i].Attribution(i, _buttonNames[i], this);

            }
        }


        // PUBLIC

        public void ButonAction(int IDButton)
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
