using System;
using UnityEngine;
using Steamworks;
using SWExtensions;
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
                Debug.LogError("[LOBBY CREATION] starting ... ");
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
            if (BoltNetwork.IsServer)
            {
                Debug.LogError("[LOBBY DATA] sending ...");
                if (_sessionData.MySession != null)
                {
                    //SteamMatchmaking.SetLobbyData(_lobbyID, "ready", "yes");
                    SteamMatchmaking.SetLobbyData(_lobbyID, "boltLobbyId", _sessionData.MySession.Id.ToString());
                    Debug.LogError("[LOBBY DATA] BoltID  ... " + _sessionData.MySession.Id.ToString());
                }
                Debug.LogError("[LOBBY DATA] SENT !");
            }
        }

        // PROTECTED

        protected Callback<GameLobbyJoinRequested_t> GameLobbyJoinRequestedCallback;

        protected Callback<LobbyCreated_t> LobbyCreatedCallback;

        protected Callback<LobbyEnter_t> LobbyEnteredCallback;

        protected Callback<LobbyDataUpdate_t> LobbyDataUpdatedCallback;

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

            Debug.LogErrorFormat("[LOBBY ENTERED] Entered with ID {0}", result.m_ulSteamIDLobby);
        }

        private void OnLobbyDataUpdated(LobbyDataUpdate_t result)
        {
            Debug.LogError("[LOBBY DATA] Updated ...");

            //bool isReady = SteamMatchmaking.GetLobbyData(_lobbyID, "ready") == "yes";
            //Debug.LogError("[LOBBY DATA] ... Ready value received : " + isReady.ToString());

            try
            {
                Guid boltServerID = new Guid(SteamMatchmaking.GetLobbyData(_lobbyID, "boltLobbyId"));
                Debug.LogError("[LOBBY DATA] ... ID value received : " + boltServerID.ToString());

                if (!BoltNetwork.IsServer && boltServerID != null)
                {
                    Debug.LogError("[BOLT] Starting client ...");
                    BoltLauncher.StartClient();
                    StartCoroutine(JoinBoltLobby(boltServerID));
                }
            }
            catch (FormatException e)
            {
                Debug.LogError("Yo l'erreur");
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
