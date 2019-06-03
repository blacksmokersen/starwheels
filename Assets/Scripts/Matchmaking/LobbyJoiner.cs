using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using UnityEngine.UI;
using udpkit.platform.photon;

namespace SW.Matchmaking
{
    public class LobbyJoiner : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;
        [SerializeField] private GameObject _lobbyPanel;

        [Header("Events")]
        public UnityEvent OnLookingForLobby;

        [Header("DebugMode")]
        [SerializeField] private ServerDebugMode _serverDebugMode;

        [HideInInspector] public bool DebugModEnabled = false;

        // BOLT

        public override void BoltStartBegin()
        {
            if (BoltNetwork.IsClient)
            {
                Debug.Log("[BOLT] Registering tokens.");
                BoltNetwork.RegisterTokenClass<Multiplayer.RoomProtocolToken>();
                BoltNetwork.RegisterTokenClass<LobbyToken>();
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

        public void StartLookingForLobby()
        {
            StartCoroutine(LookForLobby());
        }

        public void StopLookingForLobby()
        {
            StopAllCoroutines();
        }

        public void TryJoiningLobby()
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

            if (canJoinLobby)
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

        public void FindPublicLobby()
        {
            var lobbyList = BoltNetwork.SessionList;

            foreach (var lobby in lobbyList)
            {
                var lobbyToken = SWMatchmaking.GetLobbyToken(lobby.Key);
                var lobbyMatchesSelectedServerName = DebugModEnabled && lobbyToken.ServerName == _serverDebugMode.GetClientServerName();
                var lobbyMatchesSelectedGamemodes = _lobbyData.GamemodePool.Contains(lobbyToken.GameMode);
                Debug.Log("Id " + lobby.Key.ToString());

                if ((lobbyMatchesSelectedServerName || lobbyMatchesSelectedGamemodes)
                    && lobbyToken.Public
                    && lobbyToken.CanBeJoined
                    && lobbyToken.Version.Equals(_lobbyData.Version))
                {
                    _lobbyData.SetGamemode(lobbyToken.GameMode);
                    _lobbyData.SetMap(lobbyToken.MapName);
                    SWMatchmaking.JoinLobby(lobby.Key);
                }
            }
        }

        // PRIVATE

        private IEnumerator LookForLobby()
        {
            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(0.5f);
                FindPublicLobby();
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
                StartLookingForLobby();
            }
            else if (timerExceedeed)
            {
                Debug.LogWarning("[BOLT] Took too long to connect as client.");
            }
        }
    }
}
