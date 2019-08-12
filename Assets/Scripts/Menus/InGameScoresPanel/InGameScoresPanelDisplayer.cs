using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Bolt;

namespace Menu.InGameScores
{
    public class InGameScoresPanelDisplayer : GlobalEventListener, IControllable
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

        // BOLT

        public override void OnEvent(GameOver evnt)
        {
            StartCoroutine(ActivateAfterXSecondsRoutine(5f));
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

        public void ShowPanel()
        {
            _panel.SetActive(true);
            foreach (var rect in GetComponentsInChildren<RectTransform>())
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            }
        }

        public void HidePanel()
        {
            _panel.SetActive(false);
        }

        // PRIVATE

        private IEnumerator ActivateAfterXSecondsRoutine(float x)
        {
            yield return new WaitForSeconds(x);
            ShowPanel();
            Enabled = false;
        }
    }
}
