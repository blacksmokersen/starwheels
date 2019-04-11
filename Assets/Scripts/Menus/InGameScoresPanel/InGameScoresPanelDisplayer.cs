using UnityEngine;
using UnityEngine.UI;

namespace Menu.InGameScores
{
    public class InGameScoresPanelDisplayer : MonoBehaviour, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("UI Elements")]
        [SerializeField] private GameObject _panel;

        // CORE

        private void Update()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled)
            {
                if (Input.GetButtonDown(Constants.Input.Select))
                {
                    ShowPanel();
                }
                else if (Input.GetButtonUp(Constants.Input.Select))
                {
                    HidePanel();
                }
            }
        }

        // PRIVATE

        private void ShowPanel()
        {
            _panel.SetActive(true);
            foreach (var rect in GetComponentsInChildren<RectTransform>())
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            }
        }

        private void HidePanel()
        {
            _panel.SetActive(false);
        }
    }
}
