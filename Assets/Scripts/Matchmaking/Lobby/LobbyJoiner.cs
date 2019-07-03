using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using UnityEngine.UI;
using Multiplayer;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class LobbyJoiner : GlobalEventListener
    {
        [Header("Settings")]
        [SerializeField] private MatchmakingSettings _matchmakingSettings;
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;
        [SerializeField] private GameObject _lobbyPanel;

        [Header("Events")]
        public UnityEvent OnLookingForLobby;

        [HideInInspector] public bool DebugModEnabled = false;

        // BOLT

        public override void BoltStartBegin()
        {
            if (BoltNetwork.IsClient)
            {
                Debug.Log("[BOLT] Registering tokens.");
                BoltNetwork.RegisterTokenClass<RoomProtocolToken>();
                BoltNetwork.RegisterTokenClass<LobbyToken>();
                BoltNetwork.RegisterTokenClass<JoinToken>();
            }
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            StopAllCoroutines();
        }

        // PUBLIC

        public void SetRegion(int regionID)
        {
            SWMatchmaking.SetRegion(regionID);
        }

        public void StartLookingForGame()
        {
            StartCoroutine(LookForGame());
        }

        public void StopLookingForLobby()
        {
            StopAllCoroutines();
        }

        public void TryJoiningGame()
        {
            bool canJoinLobby = false;
            foreach (var toggle in _lobbyPanel.GetComponentsInChildren<Toggle>())
            {
                if (toggle.isOn)
                {
                    canJoinLobby = true;
                    break;
                }
            }

            if (canJoinLobby || _matchmakingSettings.LookForPrivateGames)
            {
                _lobbyPanel.SetActive(false);
                ConnectAsClient();
            }
            else
            {
                Debug.LogError("Cannot join lobby if no Gamemode is selected.");
            }
        }

        public void ConnectAsClient()
        {
            if (!BoltNetwork.IsConnected)
            {
                StartCoroutine(WaitForConnectedAsClient());
            }
            else
            {
                Debug.LogWarning("Can't connect as client when Bolt is already running.");
            }
        }

        public void FindGame()
        {
            var lobbyList = BoltNetwork.SessionList;

            foreach (var lobby in lobbyList)
            {
                var lobbyToken = SWMatchmaking.GetLobbyToken(lobby.Key);
                Debug.Log("[LOBBY] Name : " + lobbyToken.ServerName);
                var lobbyMatchesSelectedServerName = _matchmakingSettings.LookForPrivateGames && lobbyToken.ServerName == _matchmakingSettings.PrivateGameName;
                Debug.Log("[LOBBY] Matched : " + lobbyMatchesSelectedServerName);
                var lobbyMatchesSelectedGamemodes = !_matchmakingSettings.LookForPrivateGames && lobbyToken.Public && _lobbyData.GamemodePool.Contains(lobbyToken.GameMode);
                var lobbyMatchesMatchmakingSettings = (!lobbyToken.GameStarted || lobbyToken.GameStarted == _matchmakingSettings.LookForStartedGames);

                if ((lobbyMatchesSelectedServerName || lobbyMatchesSelectedGamemodes)
                    && lobbyMatchesMatchmakingSettings
                    && lobbyToken.CanBeJoined
                    && lobbyToken.Version.Equals(_lobbyData.Version))
                {
                    _lobbyData.SetGamemode(lobbyToken.GameMode);
                    _lobbyData.SetMap(lobbyToken.MapName);
                    SWMatchmaking.JoinLobby
                     (
                        lobby.Key,
                        new JoinToken()
                        {
                            Nickname = _playerSettings.Nickname
                        }
                    );
                }
            }
        }

        // PRIVATE

        private IEnumerator LookForGame()
        {
            _matchmakingSettings.LookForStartedGames = false;
            float step = 0.5f;
            var timer = 0f;

            while (Application.isPlaying)
            {
                FindGame();

                yield return new WaitForSeconds(step);
                timer += step;

                if (timer > 10f)
                {
                    _matchmakingSettings.LookForStartedGames = true;
                }
            }
        }

        private IEnumerator WaitForConnectedAsClient()
        {
            var timer = 0f;
            bool timerExceedeed = false;

            BoltLauncher.StartClient();
            while (!BoltNetwork.IsClient && !timerExceedeed)
            {
                timer += Time.deltaTime;
                if (timer > 10f)
                {
                    timerExceedeed = true;
                }
                yield return new WaitForEndOfFrame();
            }

            if (BoltNetwork.IsClient)
            {
                if (OnLookingForLobby != null)
                {
                    OnLookingForLobby.Invoke();
                    Debug.Log("[BOLT] Bolt now running as client.");
                }
                StartLookingForGame();
            }
            else if (timerExceedeed)
            {
                Debug.LogWarning("[BOLT] Took too long to connect as client.");
            }
        }
    }
}
