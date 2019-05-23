using UnityEngine;
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
        [SerializeField] private GameObject _startGameButton;

        // BOLT

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            gameObject.SetActive(false);
        }

        public override void Connected(BoltConnection connection)
        {
            if (connection.ConnectionId == SWMatchmaking.GetMyBoltId())
            {
                SetLookingForPlayers();
            }

            if (BoltNetwork.IsServer)
            {
                var playerCount = 1 + SWMatchmaking.GetCurrentLobbyPlayerCount();
                LobbyPlayerJoined lobbyPlayerJoinedEvent = LobbyPlayerJoined.Create();
                lobbyPlayerJoinedEvent.LobbyPlayerCount = playerCount;
                lobbyPlayerJoinedEvent.PlayerID = (int)connection.ConnectionId;
                lobbyPlayerJoinedEvent.Send();

                UpdateCurrentPlayerCount(playerCount);
            }
        }

        public override void Disconnected(BoltConnection connection)
        {
            if (BoltNetwork.IsServer)
            {
                var playerCount = SWMatchmaking.GetCurrentLobbyPlayerCount();
                LobbyPlayerLeft lobbyPlayerLeftEvent = LobbyPlayerLeft.Create();
                lobbyPlayerLeftEvent.LobbyPlayerCount = playerCount;
                lobbyPlayerLeftEvent.PlayerID = (int)connection.ConnectionId;
                lobbyPlayerLeftEvent.Send();

                UpdateCurrentPlayerCount(playerCount);
            }
        }

        public override void OnEvent(LobbyPlayerJoined evnt)
        {
            if (BoltNetwork.IsClient)
            {
                UpdateCurrentPlayerCount(evnt.LobbyPlayerCount);
            }
            Debug.Log("A player joined the lobby.");
        }

        public override void OnEvent(LobbyPlayerLeft evnt)
        {
            if (BoltNetwork.IsClient)
            {
                UpdateCurrentPlayerCount(evnt.LobbyPlayerCount);
            }
            Debug.Log("A player disconnected from the lobby.");
        }

        // PUBLIC

        public void SetLookingForGame()
        {
            gameObject.SetActive(true);
            _lookingForGameText.gameObject.SetActive(true);
            _currentPlayerCountText.gameObject.SetActive(false);
            _startGameButton.SetActive(false);
        }

        public void SetLookingForPlayers()
        {
            gameObject.SetActive(true);
            _lookingForGameText.gameObject.SetActive(false);
            _currentPlayerCountText.gameObject.SetActive(true);
            _startGameButton.SetActive(BoltNetwork.IsServer);
        }

        public void LaunchGame()
        {
            if (BoltNetwork.IsServer)
            {
                var roomToken = new Multiplayer.RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode };
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

        private void UpdateCurrentPlayerCount(int playerCount)
        {
            _currentPlayerCountText.text = playerCount + " players";
        }

        private void OnApplicationQuit()
        {
            QuitLobby();
        }
    }
}
