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
