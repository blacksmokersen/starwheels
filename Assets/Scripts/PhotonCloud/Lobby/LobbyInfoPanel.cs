using UnityEngine;
using UnityEngine.UI;

namespace Photon.Lobby
{
    public class LobbyInfoPanel : MonoBehaviour
    {
        [SerializeField] private Text _infoText;
        [SerializeField] private Text _buttonText;
        [SerializeField] private Button _singleButton;

        public void Display(string info, string buttonInfo, UnityEngine.Events.UnityAction buttonClbk)
        {
            _infoText.text = info;

            _buttonText.text = buttonInfo;

            _singleButton.onClick.RemoveAllListeners();

            if (buttonClbk != null)
            {
                _singleButton.onClick.AddListener(buttonClbk);
            }

            _singleButton.onClick.AddListener(() => { gameObject.SetActive(false); });

            gameObject.SetActive(true);
        }
    }
}
