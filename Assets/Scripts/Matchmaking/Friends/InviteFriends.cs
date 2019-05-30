using System;
using UnityEngine;
using Steamworks;
using System.Collections;

namespace SW.Matchmaking.Friends
{
    public class InviteFriends : MonoBehaviour
    {
        public int CurrentFriendCount = 0;

        [Header("Session")]
        [SerializeField] private SessionData _sessionData;

        [Header("Group Settings")]
        [SerializeField] private int _maxFriends;

        private CSteamID _lobbyID;
        private bool _lobbyCreated = false;

        // CALLBACKS

        protected Callback<GameLobbyJoinRequested_t> GameLobbyJoinRequestedCallback;

        protected Callback<LobbyCreated_t> LobbyCreatedCallback;

        protected Callback<LobbyEnter_t> LobbyEnteredCallback;

        protected Callback<LobbyDataUpdate_t> LobbyDataUpdatedCallback;

        // PUBLIC

        public void InitializeCallbacks()
        {
            if (SteamManager.Initialized)
            {
                GameLobbyJoinRequestedCallback = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
                LobbyCreatedCallback = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
                LobbyEnteredCallback = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
                LobbyDataUpdatedCallback = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdated);
                Debug.LogError("[INITIALIZATION] Done !");
            }
            else
            {
                Debug.LogWarning("Could not initialize callbacks since Steam is not initialized.");
            }
        }

        public void CreateSteamFriendsLobby()
        {
            if (SteamManager.Initialized)
            {
                Debug.LogError("[LOBBY CREATION] Starting ... ");
                SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _maxFriends);
                Debug.LogError("[LOBBY CREATION] Sent event ! ");
            }
        }

        public void OpenInvitationPopup()
        {
            if (SteamManager.Initialized && _lobbyCreated)
            {
                Debug.LogError("Opening invitation popup ...");
                SteamFriends.ActivateGameOverlayInviteDialog(_lobbyID);

                Debug.LogError("Friends online : " + SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate));
            }
        }

        public void SendBoltLobbyInfoToFriends() // Send the server Bolt ID to the friends lobby, so that they can join
        {
            Debug.LogError("[LOBBY DATA] sending ...");
            if (_sessionData.MySession != null)
            {
                var randomServerID = UnityEngine.Random.Range(0, 255).ToString();
                SteamMatchmaking.SetLobbyData(_lobbyID, "boltLobbyId", randomServerID);
                Debug.LogError("[LOBBY DATA] sending to ... " + _lobbyID.ToString());
                Debug.LogError("[LOBBY DATA] Random Server ID : " + randomServerID);
                Debug.LogError("[LOBBY DATA] SENT !");
            }
        }

        // PRIVATE

        private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t result)
        {
            Debug.LogErrorFormat("[JOINING LOBBY] Attempt with id {0}", result.m_steamIDLobby.ToString());
            SteamMatchmaking.JoinLobby(result.m_steamIDLobby);
        }

        private void OnLobbyCreated(LobbyCreated_t result)
        {
            if (result.m_eResult == EResult.k_EResultOK)
            {
                _lobbyID = (CSteamID)result.m_ulSteamIDLobby;
                _lobbyCreated = true;
                OpenInvitationPopup();
                Debug.LogError("[LOBBY CREATION] DONE ! ");
            }
            else
            {
                Debug.LogWarning("Couldn't create lobby.");
            }
        }

        private void OnLobbyEntered(LobbyEnter_t result)
        {
            _lobbyID = (CSteamID)result.m_ulSteamIDLobby;

            Debug.LogErrorFormat("[LOBBY ENTERED] Entered with SteamID {0}", _lobbyID.ToString());
            Debug.LogErrorFormat("[LOBBY ENTERED] Current bolt lobby ID value : {0} ", SteamMatchmaking.GetLobbyData(_lobbyID, "boltLobbyId"));
        }

        private void OnLobbyDataUpdated(LobbyDataUpdate_t result)
        {
            Debug.LogError("[LOBBY DATA] Updated ...");

            try
            {
                string boltServerID = SteamMatchmaking.GetLobbyData(_lobbyID, "boltLobbyId");
                Debug.LogError("[LOBBY DATA] ... ID value received : " + boltServerID);

                if (!BoltNetwork.IsServer && boltServerID != "")
                {
                    Debug.LogError("[BOLT] Starting client ...");
                    BoltLauncher.StartClient();
                    StartCoroutine(JoinBoltLobby(new Guid(boltServerID)));
                }
            }
            catch (FormatException e)
            {
                Debug.LogError("[ERROR] Error creating Guid for server");
            }
        }

        private IEnumerator JoinBoltLobby(Guid boltServerID)
        {
            while (!(BoltNetwork.IsConnected && BoltNetwork.IsClient))
            {
                yield return new WaitForEndOfFrame();
            }
            Debug.LogError("[BOLT] Joining lobby !");
            SWMatchmaking.JoinLobby(boltServerID);
        }
    }
}
