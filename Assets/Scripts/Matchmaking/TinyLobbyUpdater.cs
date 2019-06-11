using UdpKit;
using System;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using TMPro;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class TinyLobbyUpdater : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("UI Text Elements")]
        [SerializeField] private TextMeshProUGUI _lookingForGameText;
        [SerializeField] private TextMeshProUGUI _currentGamemodeLobbiesText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _currentPlayerCountText;

        [Header("UI Buttons")]
        [SerializeField] private GameObject _inviteFriendsButton;
        [SerializeField] private GameObject _startGameButton;

        [Header("Settings")]
        [SerializeField] private int _minimumPlayersToAutomaticLaunch;
        [SerializeField] private float _secondsBeforeLaunchingGame;
        [SerializeField] private float _secondsBeforeCreatingGame;

        [Header("Events")]
        public UnityEvent OnAutomaticLaunch;
        public UnityEvent OnNoLobbyFound;

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

            if (BoltNetwork.IsServer)
            {
                SetLookingForPlayers();
            }
            else
            {
                SetLookingForGame();
            }
        }

        // BOLT

        public override void SessionConnected(UdpSession session, IProtocolToken token)
        {
            Debug.LogError("Session connected.");
            gameObject.SetActive(true);
        }

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
            UpdateCurrentMatchedLobbies(matchedLobbyCount);
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
            _lookingForGameText.text = "Looking for a game";
            _currentPlayerCountText.gameObject.SetActive(false);
            _startGameButton.SetActive(false);
            _inviteFriendsButton.gameObject.SetActive(false);

            _timerStarted = true;
        }

        public void SetLookingForPlayers()
        {
            gameObject.SetActive(true);
            _lookingForGameText.gameObject.SetActive(true);
            _lookingForGameText.text = "Looking for players";
            _currentPlayerCountText.gameObject.SetActive(true);
            _timerText.gameObject.SetActive(BoltNetwork.IsServer);
            UpdateCurrentPlayerCount(1);
            _startGameButton.SetActive(BoltNetwork.IsServer);
            _inviteFriendsButton.gameObject.SetActive(BoltNetwork.IsServer);

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
            if (BoltNetwork.IsServer && _timer > _secondsBeforeLaunchingGame && (1+SWMatchmaking.GetCurrentLobbyPlayerCount()) >= _minimumPlayersToAutomaticLaunch)
            {
                ResetTimer();
                if (OnAutomaticLaunch != null)
                {
                    OnAutomaticLaunch.Invoke();
                }
            }
            else if (BoltNetwork.IsClient && _timer > _secondsBeforeCreatingGame)
            {
                ResetTimer();
                if (OnNoLobbyFound != null)
                {
                    OnNoLobbyFound.Invoke();
                }
            }
        }

        private void UpdateCurrentMatchedLobbies(int lobbyCount)
        {
            var gamemodes = String.Join("/", _lobbyData.GamemodePool.ToArray());
            _currentGamemodeLobbiesText.text = lobbyCount + " lobbies found for " + gamemodes + " gamemode(s).";
        }

        private void OnApplicationQuit()
        {
            QuitLobby();
        }
    }
}
