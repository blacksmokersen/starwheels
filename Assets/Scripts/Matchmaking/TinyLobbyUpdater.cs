using UnityEngine;
using UnityEngine.UI;
using Bolt;
using TMPro;

namespace SW.Matchmaking
{
    public class TinyLobbyUpdater : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _lookingForGameText;
        [SerializeField] private TextMeshProUGUI _currentPlayerCountText;
        [SerializeField] private Button _startGameButton;

        // CORE

        private new void OnEnable()
        {
            base.OnEnable();

            if (BoltNetwork.IsConnected)
            {
                if (BoltNetwork.IsServer)
                {
                    SetLookingForGame(false);
                    _startGameButton.gameObject.SetActive(true);
                }
                else
                {
                    SetLookingForGame(true);
                    _startGameButton.gameObject.SetActive(false);
                }
            }
        }

        // BOLT

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            gameObject.SetActive(false);
        }

        public override void Connected(BoltConnection connection)
        {

            Debug.Log("A player joined the lobby.");
            UpdateCurrentPlayerCount();
        }

        public override void Disconnected(BoltConnection connection)
        {
            Debug.Log("A player disconnected from the lobby.");
            UpdateCurrentPlayerCount();
        }

        // PUBLIC

        public void LaunchGame()
        {
            if (BoltNetwork.IsServer)
            {
                var roomToken = new Photon.RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode };
                BoltNetwork.LoadScene(_lobbyData.ChosenMapName, roomToken);
            }
            else
            {
                Debug.LogWarning("Can't launch game if you are not the server.");
            }
        }

        public void QuitLobby()
        {
            BoltLauncher.Shutdown();
        }

        // PRIVATE

        private void UpdateCurrentPlayerCount()
        {
            var playerCount = 1 + SWMatchmaking.GetCurrentLobbyPlayerCount();
            _currentPlayerCountText.text = playerCount + " players";
        }

        private void SetLookingForGame(bool b)
        {
            _lookingForGameText.gameObject.SetActive(b);
            _currentPlayerCountText.gameObject.SetActive(!b);
        }

        private void OnApplicationQuit()
        {
            QuitLobby();
        }
    }
}
