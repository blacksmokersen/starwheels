using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using System;
using UdpKit;
using UnityEngine.UI;

namespace SW.Matchmaking
{
    public class LobbyJoiner : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;
        [SerializeField] private GameObject _lobbyPanel;

        [Header("Events")]
        public UnityEvent OnConnectedAsClient;

        [Header("DebugMode")]
        [SerializeField] private ServerDebugMode _serverDebugMode;

        [HideInInspector] public bool DebugModEnabled = false;

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
            }

            StartCoroutine(LobbyCountDebug());
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            StopAllCoroutines();
        }

        public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
        {
            Debug.Log("Session count updated : " + sessionList.Count);
        }

        // PUBLIC

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

            if (DebugModEnabled)
            {
                foreach (var lobby in lobbyList)
                {
                    var lobbyToken = SWMatchmaking.GetLobbyToken(lobby.Key);
                    Debug.Log("Found lobby for ServerName : " + _serverDebugMode.GetClientServerName());
                    if (lobbyToken.ServerName == _serverDebugMode.GetClientServerName())
                    {
                        Debug.Log("Found ServerName : " + lobbyToken.ServerName);
                        _lobbyData.SetGamemode(lobbyToken.GameMode);
                        _lobbyData.SetMap(lobbyToken.MapName);
                        SWMatchmaking.JoinLobby(lobby.Key);
                    }
                }
            }
            else
            {
                foreach (var lobby in lobbyList)
                {
                    var lobbyToken = SWMatchmaking.GetLobbyToken(lobby.Key);
                    Debug.Log("Found lobby for : " + lobbyToken.GameMode);
                    if (_lobbyData.GamemodePool.Contains(lobbyToken.GameMode))
                    {
                        _lobbyData.SetGamemode(lobbyToken.GameMode);
                        _lobbyData.SetMap(lobbyToken.MapName);
                        SWMatchmaking.JoinLobby(lobby.Key);
                    }
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
