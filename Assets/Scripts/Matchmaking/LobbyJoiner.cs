using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using System;
using UdpKit;

namespace SW.Matchmaking
{
    public class LobbyJoiner : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("Events")]
        public UnityEvent OnConnectedAsClient;

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
                OnConnectedAsClient.Invoke();
                Debug.Log("Bolt now running as client.");
                Debug.LogError("Sessions found : " + BoltNetwork.SessionList.Count);
            }

            StartCoroutine(LobbyCountDebug());
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            StopAllCoroutines();
        }

        public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
        {
            Debug.LogError("Session count updated : " + sessionList.Count);
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
