using UnityEngine;
using UnityEngine.UI;

namespace Photon.Lobby
{
    public class LobbyTopPanel : MonoBehaviour
    {
        public bool IsInGame = false;

        private bool _isDisplayed = true;
        private Image _panelImage;

        // CORE

        private void Start()
        {
            _panelImage = GetComponent<Image>();
        }

        private void Update()
        {
            if (!IsInGame)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleVisibility(!_isDisplayed);
            }
        }

        // PUBLIC

        public void ToggleVisibility(bool visible)
        {
            _isDisplayed = visible;

            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(_isDisplayed);
            }

            if (_panelImage != null)
            {
                _panelImage.enabled = _isDisplayed;
            }
        }
    }
}
