using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using System;
using UdpKit;

namespace SW.Matchmaking
{
    public class LobbyJoiner : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        // BOLT

        public override void BoltStartBegin()
        {
            if (BoltNetwork.IsClient)
            {
                Debug.Log("Registering tokens...");
                BoltNetwork.RegisterTokenClass<Photon.RoomProtocolToken>();
                BoltNetwork.RegisterTokenClass<Photon.ServerAcceptToken>();
                BoltNetwork.RegisterTokenClass<Photon.ServerConnectToken>();
                BoltNetwork.RegisterTokenClass<SW.Matchmaking.LobbyToken>();
            }

        }



        public override void BoltStartDone()
        {
            if (BoltNetwork.IsClient)
            {
                Debug.Log("Bolt now running as client.");
                FindLobby();
            }

            StartCoroutine(LobbyCountDebug());
        }

        public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
        {
            Debug.LogError("Session count : " + sessionList.Count);
            FindLobby();
        }

        // PUBLIC

        public void ConnectAsClient()
        {
            if (!BoltNetwork.IsConnected)
            {
                BoltLauncher.StartClient();
            }
            else
            {
                Debug.LogWarning("Can't connect as client when Bolt is already running.");
            }
        }

        public void FindLobby()
        {
            var lobbyList = BoltNetwork.SessionList;
            Debug.LogError("Lobbies found : " + lobbyList.Count);

            foreach (var lobby in lobbyList)
            {
                var lobbyToken = SWMatchmaking.GetLobbyToken(lobby.Key);
                if (_lobbyData.GamemodePool.Contains(lobbyToken.GameMode))
                {
                    SWMatchmaking.JoinLobby(lobby.Key);
                }
            }
        }

        // PRIVATE

        private IEnumerator LobbyCountDebug()
        {
            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(0.5f);
                FindLobby();
            }
        }
    }
}
