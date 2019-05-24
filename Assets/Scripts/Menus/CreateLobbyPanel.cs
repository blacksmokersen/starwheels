using UnityEngine;

namespace Menu
{
    public class CreateLobbyPanel : MonoBehaviour
    {
        [SerializeField] private bool _resetOnEnable;

        [Header("UI Elements")]
        [SerializeField] private GameObject _gamemodePanel;
        [SerializeField] private GameObject _mapsPanel;
        [SerializeField] private GameObject _battleMapsPanel;
        [SerializeField] private GameObject _orbMapsPanel;

        // CORE

        private void OnEnable()
        {
            if (_resetOnEnable)
            {
                ResetPanel(true);
            }
        }

        // PUBLIC

        public void ResetPanel(bool letSelfActive)
        {
            _battleMapsPanel.SetActive(false);
            _orbMapsPanel.SetActive(false);
            _mapsPanel.SetActive(true);
            _gamemodePanel.SetActive(true);

            gameObject.SetActive(letSelfActive);
        }
    }
}
