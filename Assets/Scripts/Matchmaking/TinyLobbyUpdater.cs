using UdpKit;
using System;
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
        [SerializeField] private GameObject _createGamePanel;
        [SerializeField] private TextMeshProUGUI _lookingForGameText;
        [SerializeField] private TextMeshProUGUI _currentGamemodeLobbiesText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _currentPlayerCountText;
        [SerializeField] private GameObject _startGameButton;

        [Header("Settings")]
        [SerializeField] private float _secondsBeforeCreatingGame;

        private bool _timerStarted = false;
        private float _timer = 0.0f;

        // MONO

        private void Update()
        {
            if (_timerStarted)
            {
                _timer += Time.deltaTime;
                UpdateTimer(_timer);
                CheckTimer();
            }
        }

        private new void OnEnable()
        {
            base.OnEnable();
            ResetTimer();
        }

        // BOLT

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            gameObject.SetActive(false);
        }

        public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
        {
            LobbyToken lobbyToken;
            int matchedLobbyCount = 0;
            foreach (var lobby in sessionList)
            {
                lobbyToken = SWMatchmaking.GetLobbyToken(lobby.Key);

                if (_lobbyData.GamemodePool.Contains(lobbyToken.GameMode))
                {
                    matchedLobbyCount++;
                }
            }
        }

        public override void Connected(BoltConnection connection)
        {
            if (connection.ConnectionId == SWMatchmaking.GetMyBoltId())
            {
                SetLookingForPlayers();
                ResetTimer();
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

            _timerStarted = true;
        }

        public void SetLookingForPlayers()
        {
            gameObject.SetActive(true);
            _lookingForGameText.gameObject.SetActive(false);
            _currentPlayerCountText.gameObject.SetActive(true);
            _startGameButton.SetActive(BoltNetwork.IsServer);

            _timerStarted = true;
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
            ResetTimer();
            BoltLauncher.Shutdown();
        }

        // PRIVATE

        private void UpdateCurrentPlayerCount(int playerCount)
        {
            _currentPlayerCountText.text = playerCount + " players";
        }

        private void UpdateTimer(float seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            DateTime dateTime = DateTime.Today.Add(time);
            _timerText.text = dateTime.ToString("mm:ss");
        }

        private void ResetTimer()
        {
            _timerStarted = false;
            _timer = 0f;
            UpdateTimer(_timer);
        }

        private void CheckTimer()
        {
            if (BoltNetwork.IsClient && _timer > _secondsBeforeCreatingGame)
            {
                _createGamePanel.SetActive(true);
            }
        }

        private void UpdateCurrentMatchedLobbies(int lobbyCount)
        {
            var gamemodes = String.Join("/", _lobbyData.GamemodePool.ToArray());
            _currentGamemodeLobbiesText.text = lobbyCount + " lobbies found for " + gamemodes;
        }

        private void OnApplicationQuit()
        {
            QuitLobby();
        }
    }
}
