using System;
using UnityEngine;
using Steamworks;
using System.Collections;
using Bolt;
using System.Text;

namespace SW.Matchmaking.Friends
{
    [DisallowMultipleComponent]
    public class InviteFriends : GlobalEventListener
    {
        [Header("Session")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("Group Settings")]
        [SerializeField] private int _maxFriends;

        protected Callback<GameLobbyJoinRequested_t> GameLobbyJoinRequestedCallback;
        protected Callback<LobbyCreated_t> LobbyCreatedCallback;
        protected Callback<LobbyEnter_t> LobbyEnteredCallback;
        protected Callback<LobbyDataUpdate_t> LobbyDataUpdatedCallback;
        protected Callback<LobbyChatMsg_t> LobbyChatMsgCallback;

        private const string _lobbyNameParameterName = "BoltLobbyID";
        private const string _lobbyKickAllFlag = "KickAll";

        private CSteamID _steamLobbyID;

        // BOLT

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            if (BoltNetwork.IsServer)
            {
                SendKickEveryoneFlag();
                QuitSteamLobby();
            }
        }

        // CALLBACKS

        private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t result)
        {
            Debug.LogErrorFormat("[STEAM] Join request from ({0}).", result.m_steamIDLobby.ToString());
            SteamMatchmaking.JoinLobby(result.m_steamIDLobby);
        }

        private void OnLobbyCreated(LobbyCreated_t result)
        {
            if (result.m_eResult == EResult.k_EResultOK)
            {
                _steamLobbyID = (CSteamID)result.m_ulSteamIDLobby;
                SetBoltLobbyID();
                OpenInvitationPopup();
                Debug.LogError("[STEAM] Lobby created.");
            }
            else
            {
                Debug.LogWarning("[STEAM] Couldn't create lobby.");
            }
        }

        private void OnLobbyEntered(LobbyEnter_t result)
        {
            _steamLobbyID = (CSteamID)result.m_ulSteamIDLobby;
            Debug.LogErrorFormat("[STEAM] Entered lobby with SteamID {0}", _steamLobbyID.ToString());
        }

        private void OnLobbyDataUpdated(LobbyDataUpdate_t result)
        {
            if (!BoltNetwork.IsServer)
            {
                string serverName = SteamMatchmaking.GetLobbyData(_steamLobbyID, _lobbyNameParameterName);
                Debug.LogError("[STEAM] Bolt server name updated : " + serverName);

                if (serverName != "")
                {
                    if (!BoltNetwork.IsClient)
                    {
                        Debug.LogError("[BOLT] Starting client ...");
                        BoltLauncher.StartClient();
                    }
                    StartCoroutine(JoinBoltLobby(serverName));
                }
            }
            else
            {
                string boltServerID = SteamMatchmaking.GetLobbyData(_steamLobbyID, _lobbyNameParameterName);
                Debug.LogError("[STEAM] Bolt server name successfully updated : " + boltServerID);
            }
        }

        private void OnLobbyChatMsg(LobbyChatMsg_t result)
        {
            byte[] data = new byte[4096];
            CSteamID senderID;
            EChatEntryType eChatEntryType;
            SteamMatchmaking.GetLobbyChatEntry(_steamLobbyID, (int)result.m_iChatID, out senderID, data, data.Length, out eChatEntryType);

            Debug.Log("[STEAM] Received message : " + Encoding.UTF8.GetString(data) + ".");
            if (Encoding.UTF8.GetString(data).Equals(_lobbyKickAllFlag))
            {
                Debug.Log("[STEAM] Received SteamKicked event.");
                QuitSteamLobby();
                BoltLauncher.Shutdown();
            }
        }

        // PUBLIC

        public void InitializeCallbacks()
        {
            if (SteamManager.Initialized)
            {
                GameLobbyJoinRequestedCallback = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
                LobbyCreatedCallback = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
                LobbyEnteredCallback = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
                LobbyDataUpdatedCallback = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdated);
                LobbyChatMsgCallback = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMsg);

                Debug.Log("[STEAM] Callbacks initialization done.");
            }
            else
            {
                Debug.LogWarning("[STEAM] Could not initialize callbacks since Steam is not initialized.");
            }
        }

        public void CreateSteamFriendsLobby()
        {
            if (SteamManager.Initialized)
            {
                Debug.LogError("[STEAM] Initializing lobby creation ... ");
                SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _maxFriends);
            }
        }

        public void OpenInvitationPopup()
        {
            if (SteamManager.Initialized)
            {
                Debug.LogError("[STEAM] Opening invitation popup.");
                SteamFriends.ActivateGameOverlayInviteDialog(_steamLobbyID);
            }
        }

        public void SetBoltLobbyID() // Send the server Bolt ID to the friends lobby, so that they can join
        {
            if (BoltNetwork.IsServer)
            {
                SteamMatchmaking.SetLobbyData(_steamLobbyID, _lobbyNameParameterName, _lobbyData.ServerName);
                Debug.LogErrorFormat("[STEAM] Sending Bolt server ID ({0}) to ({1}).", _lobbyData.ServerName, _steamLobbyID.ToString());
            }
        }

        public void QuitSteamLobby()
        {
            SteamMatchmaking.LeaveLobby(_steamLobbyID);
            StopAllCoroutines();
        }

        // PRIVATE

        private void SendKickEveryoneFlag()
        {
            SteamMatchmaking.SendLobbyChatMsg(_steamLobbyID, Encoding.UTF8.GetBytes(_lobbyKickAllFlag), _lobbyKickAllFlag.Length + 1);
            Debug.Log("[STEAM] Kick everyone in SteamLobby.");
        }

        private IEnumerator JoinBoltLobby(string serverName)
        {
            while (!(BoltNetwork.IsClient && BoltNetwork.SessionList.Count > 0))
            {
                yield return new WaitForEndOfFrame();
            }
            Debug.LogError("[BOLT] Joining lobby ...");
            SWMatchmaking.JoinLobby(serverName);
        }
    }
}
