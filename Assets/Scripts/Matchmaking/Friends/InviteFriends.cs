﻿using UnityEngine;
using Steamworks;
using SWExtensions;

namespace SW.Matchmaking.Friends
{
    public class InviteFriends : MonoBehaviour
    {
        public int CurrentFriendCount = 0;

        [Header("Group Settings")]
        [SerializeField] private int _maxFriends;

        private CSteamID _lobbyID;
        private bool _lobbyCreated = false;

        // CORE

        private void Start()
        {
            if (SteamManager.Initialized)
            {
                LobbyCreatedCallback = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
                LobbyEnteredCallback = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
                LobbyDataUpdatedCallback = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdated);
            }
        }

        // PUBLIC

        public void CreateSteamFriendsLobby()
        {
            if (SteamManager.Initialized)
            {
                Debug.Log("[LOBBY CREATION] starting ... ");
                SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _maxFriends);
                Debug.Log("[LOBBY CREATION] Sent event ! ");
            }
        }

        public void OpenInvitationPopup()
        {
            if (SteamManager.Initialized && _lobbyCreated)
            {
                Debug.Log("Opening invitation popup ...");
                SteamFriends.ActivateGameOverlayInviteDialog(_lobbyID);

                Debug.Log("Friends online : " + SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate));
            }
        }

        public void SendBoltLobbyInfoToFriends() // Send the server Bolt ID to the friends lobby, so that they can join
        {
            Debug.Log("[LOBBY DATA] sending ...");
            SteamMatchmaking.SetLobbyData(_lobbyID, "boltLobbyId", 101.ToString());
            Debug.Log("[LOBBY DATA] SENT !");
        }

        // PROTECTED

        protected Callback<LobbyCreated_t> LobbyCreatedCallback;

        protected Callback<LobbyEnter_t> LobbyEnteredCallback;

        protected Callback<LobbyDataUpdate_t> LobbyDataUpdatedCallback;

        // PRIVATE

        private void OnLobbyCreated(LobbyCreated_t result)
        {
            if (result.m_eResult == EResult.k_EResultOK)
            {
                _lobbyID = (CSteamID)result.m_ulSteamIDLobby;
                _lobbyCreated = true;
                OpenInvitationPopup();
                Debug.Log("[LOBBY CREATION] DONE ! ");
            }
            else
            {
                Debug.LogWarning("Couldn't create lobby.");
            }
        }

        private void OnLobbyEntered(LobbyEnter_t result)
        {
            _lobbyID = (CSteamID)result.m_ulSteamIDLobby;

            Debug.Log("Joined friend.");
        }

        private void OnLobbyDataUpdated(LobbyDataUpdate_t result)
        {
            bool isReady = SteamMatchmaking.GetLobbyData(_lobbyID, "ready") == "yes";
            int boltID;
            Debug.Log("[LOBBY DATA] Updated ...");
            int.TryParse(SteamMatchmaking.GetLobbyData(_lobbyID, "boltLobbyId"), out boltID);
            Debug.Log("[LOBBY DATA] ... value received : " + boltID);

            if (isReady && boltID != -1)
            {
                SWMatchmaking.JoinLobby(boltID.ToGuid());
            }
        }
    }
}
