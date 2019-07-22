using UdpKit;
using System;
using UnityEngine;
using UnityEngine.Events;
using Multiplayer;
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
        private bool _createGamePanelOpen = false;

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
            _createGamePanelOpen = false;

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
        }

        // PUBLIC

        public void SetLookingForGame()
        {
            gameObject.SetActive(true);
            _lookingForGameText.gameObject.SetActive(true);
            _lookingForGameText.text = "Looking for a game";
            _startGameButton.SetActive(false);
            _inviteFriendsButton.gameObject.SetActive(false);

            _timerStarted = true;
        }

        public void SetLookingForPlayers()
        {
            gameObject.SetActive(true);
            _lookingForGameText.gameObject.SetActive(true);
            _lookingForGameText.text = "Looking for players";
            _currentGamemodeLobbiesText.gameObject.SetActive(false);
            _timerText.gameObject.SetActive(BoltNetwork.IsServer);
            _startGameButton.SetActive(BoltNetwork.IsServer);
            _inviteFriendsButton.gameObject.SetActive(BoltNetwork.IsServer);

            ResetTimer();
        }

        public void LaunchGame()
        {
            if (BoltNetwork.IsServer)
            {
                _lobbyData.CurrentPlayers = SWMatchmaking.GetCurrentLobbyPlayerCount();
                var roomToken = new RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode, PlayersCount = _lobbyData.CurrentPlayers, RoomInfo =  "test2" };
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
            else if (BoltNetwork.IsClient && _timer > _secondsBeforeCreatingGame && !_createGamePanelOpen)
            {
                _createGamePanelOpen = true;

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
