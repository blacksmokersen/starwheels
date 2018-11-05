using UnityEngine;
using UnityEngine.UI;
using UdpKit;

namespace Photon.Lobby
{
    /*
     * UI elements of a row in the server list
     *
     */
    public class LobbyServerEntry : MonoBehaviour
    {
        [Header("Server UI Elements")]
        [SerializeField] private Text _serverInfoText;
        [SerializeField] private Text _slotInfo;
        [SerializeField] private Button _joinButton;

        // PUBLIC

		public void Populate(UdpSession match, LobbyManager lobbyManager, Color c)
		{
            _serverInfoText.text = match.HostName;

            _slotInfo.text = match.ConnectionsCurrent.ToString() + "/" + match.ConnectionsMax.ToString(); ;

            _joinButton.onClick.RemoveAllListeners();
            _joinButton.onClick.AddListener(() => { JoinMatch(match, lobbyManager); });

            GetComponent<Image>().color = c;
        }

        // PRIVATE

        private void JoinMatch(UdpSession match, LobbyManager lobbyManager)
        {
            BoltNetwork.Connect(match);

            lobbyManager.BackDelegate = lobbyManager.Shutdown;
            lobbyManager.DisplayIsConnecting();
        }
    }
}
