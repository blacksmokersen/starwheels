using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Menu.Options
{
    [DisallowMultipleComponent]
    public class BindingsPanelChoice : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Dropdown _inputChoiceDropdown;

        [Header("Settings")]
        [SerializeField] private List<GameObject> _panels;

        // CORE

        private void Awake()
        {
            _inputChoiceDropdown.onValueChanged.AddListener(EnablePanel);
        }

        // PRIVATE

        private void EnablePanel(int index)
        {
            foreach (var panel in _panels)
            {
                panel.SetActive(false);
            }
            _panels[index].SetActive(true);
        }
    }
}
