using System;
using UnityEngine;
using Steamworks;
using System.Collections;
using Bolt.Matchmaking;

namespace SW.Matchmaking.Friends
{
    public class InviteFriends : MonoBehaviour
    {
        [Header("Session")]
        [SerializeField] private SessionData _sessionData;
        [SerializeField] private LobbyData _lobbyData;

        [Header("Group Settings")]
        [SerializeField] private int _maxFriends;

        private const string _lobbyNameParameterName = "BoltLobbyID";
        private CSteamID _steamLobbyID;
        private CSteamID _mySteamID;

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
                Debug.Log("[LOBBY] Callbacks initialization done.");

                _mySteamID = SteamUser.GetSteamID();
            }
            else
            {
                Debug.LogWarning("[LOBBY] Could not initialize callbacks since Steam is not initialized.");
            }
        }

        public void CreateSteamFriendsLobby()
        {
            if (SteamManager.Initialized)
            {
                Debug.LogError("[LOBBY] Initializing lobby creation ... ");
                SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _maxFriends);
            }
        }

        public void OpenInvitationPopup()
        {
            if (SteamManager.Initialized)
            {
                Debug.LogError("[LOBBY] Opening invitation popup.");
                SteamFriends.ActivateGameOverlayInviteDialog(_steamLobbyID);
            }
        }

        public void SetBoltLobbyID() // Send the server Bolt ID to the friends lobby, so that they can join
        {
            if (BoltNetwork.IsServer)
            {
                if (_sessionData.MySession != null)
                {
                    SteamMatchmaking.SetLobbyData(_steamLobbyID, _lobbyNameParameterName, _lobbyData.ServerName);
                    Debug.LogErrorFormat("[LOBBY] Sending Bolt server ID ({0}) to ({1}).", _lobbyData.ServerName, _steamLobbyID.ToString());
                }
            }
        }

        // PRIVATE

        private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t result)
        {
            Debug.LogErrorFormat("[LOBBY] Join request from ({0}).", result.m_steamIDLobby.ToString());
            SteamMatchmaking.JoinLobby(result.m_steamIDLobby);
        }

        private void OnLobbyCreated(LobbyCreated_t result)
        {
            if (result.m_eResult == EResult.k_EResultOK)
            {
                _steamLobbyID = (CSteamID)result.m_ulSteamIDLobby;
                SetBoltLobbyID();
                OpenInvitationPopup();
                Debug.LogError("[LOBBY] Lobby created.");
            }
            else
            {
                Debug.LogWarning("[LOBBY] Couldn't create lobby.");
            }
        }

        private void OnLobbyEntered(LobbyEnter_t result)
        {
            _steamLobbyID = (CSteamID)result.m_ulSteamIDLobby;
            Debug.LogErrorFormat("[LOBBY] Entered lobby with SteamID {0}", _steamLobbyID.ToString());
        }

        private void OnLobbyDataUpdated(LobbyDataUpdate_t result)
        {
            if (!BoltNetwork.IsServer)
            {
                string serverName = SteamMatchmaking.GetLobbyData(_steamLobbyID, _lobbyNameParameterName);
                Debug.LogError("[LOBBY] Bolt server ID updated : " + serverName);

                if (serverName != "")
                {
                    Debug.LogError("[BOLT] Starting client ...");
                    BoltLauncher.StartClient();
                    try
                    {
                        StartCoroutine(JoinBoltLobby(serverName));
                    }
                    catch (FormatException)
                    {
                        Debug.LogError("[ERROR] Error creating Guid for server.");
                    }
                }
            }
            else
            {
                string boltServerID = SteamMatchmaking.GetLobbyData(_steamLobbyID, _lobbyNameParameterName);
                Debug.LogError("[LOBBY] Succes : Bolt server ID updated : " + boltServerID);
            }
        }

        private IEnumerator JoinBoltLobby(string serverName)
        {
            while (!(BoltNetwork.IsClient && BoltNetwork.SessionList.Count > 0))
            {
                yield return new WaitForEndOfFrame();
            }
            Debug.LogError("[BOLT] Joining lobby !");

            SWMatchmaking.JoinLobby(serverName);
        }
    }
}
