using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network.Ping
{
    [DisallowMultipleComponent]
    public class PlayerPingUpdater : MonoBehaviour
    {
        public List<PlayerPingInformation> PlayerPingList;

        [Header("Settings")]
        [SerializeField] private PingRequester _pingChecker;
        [SerializeField] private float _secondsToRefresh;

        // CORE

        private void Start()
        {
            StartCoroutine(RefreshPingRoutine());
        }

        private void Update()
        {
            if (BoltNetwork.IsClient)
            {
                Debug.LogErrorFormat("Ping N: {0} ms", (BoltNetwork.Server.PingNetwork * 1000));
                Debug.LogErrorFormat("Ping A: {0} ms", (BoltNetwork.Server.PingAliased * 1000));
            }
        }

        // PUBLIC

        public void AddPlayer(BoltConnection playerConnection)
        {
            Debug.LogFormat("Adding player ping information for IP Address : " + playerConnection.RemoteEndPoint.ToString());
            PlayerPingList.Add
            (
                new PlayerPingInformation
                {
                    Address = playerConnection.RemoteEndPoint.ToString(),
                    PlayerID = (int)playerConnection.ConnectionId
                }
            );
        }

        public void RemovePlayer(BoltConnection playerConnection)
        {
            PlayerPingInformation playerPingInformationToRemove = null;

            foreach (var playerPingInfo in PlayerPingList)
            {
                if (playerPingInfo.PlayerID == (int) playerConnection.ConnectionId)
                {
                    playerPingInformationToRemove = playerPingInfo;
                    break;
                }
            }
            if (playerPingInformationToRemove != null)
            {
                Debug.LogFormat("Removing player ping information for IP Address : " + playerPingInformationToRemove.Address);
                PlayerPingList.Remove(playerPingInformationToRemove);
            }
        }

        public void UpdatePlayerPing(string address, int ping)
        {
            foreach (var playerPingInfo in PlayerPingList)
            {
                if (playerPingInfo.Address == address)
                {
                    playerPingInfo.AddPing(ping);
                    break;
                }
            }
        }

        public int GetCurrentPlayerPing(int playerID)
        {
            foreach (var playerPingInfo in PlayerPingList)
            {
                if (playerPingInfo.PlayerID == playerID)
                {
                    return playerPingInfo.CurrentPing;
                }
            }
            return -1;
        }

        public int GetAveragePlayerPing(int playerID)
        {
            foreach (var playerPingInfo in PlayerPingList)
            {
                if (playerPingInfo.PlayerID == playerID)
                {
                    return playerPingInfo.AveragePing;
                }
            }
            return -1;
        }

        // PRIVATE

        private IEnumerator RefreshPingRoutine()
        {
            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(_secondsToRefresh);
                RequestPingForAllPlayers();
            }
        }

        private void RequestPingForAllPlayers()
        {
            foreach (var playerPingInfo in PlayerPingList)
            {
                _pingChecker.RequestPingForAddress(playerPingInfo.Address);
            }
        }
    }
}
