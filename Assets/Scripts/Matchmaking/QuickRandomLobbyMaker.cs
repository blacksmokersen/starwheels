using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace SW.Matchmaking
{
    public class QuickRandomLobbyMaker : GlobalEventListener
    {
        [Header("Settings")]
        [SerializeField] private LobbyData _lobbyData;
        [SerializeField] private MapListData _mapListData;

        [Header("Bolt Events")]
        public UnityEvent OnBoltShutdown;

        [Header("Other Events")]
        public UnityEvent OnSwitchedToServer;
        public UnityEvent OnSwitchedToClient;
        public UnityEvent OnGameCreated;

        // PUBLIC

        public void ShutdownBolt()
        {
            BoltLauncher.Shutdown();
            StartCoroutine(WaitForBoltShutdown());
        }

        public void SwitchToServer()
        {
            BoltLauncher.StartServer();
            StartCoroutine(WaitForConnectedAsServer());
        }

        public void SwitchToClient()
        {
            BoltLauncher.StartClient();
            StartCoroutine(WaitForConnectedAsClient());
        }

        public void CreateRandomGame()
        {
            _lobbyData.SetRandomGamemode();
            PopulateMapList();
            _lobbyData.SetRandomMap();
            SWMatchmaking.SetLobbyData(_lobbyData);
            Debug.Log("Creating random game for " + _lobbyData.ChosenGamemode);
            SWMatchmaking.CreateLobby();

            if (OnGameCreated != null)
            {
                OnGameCreated.Invoke();
            }
        }

        // PRIVATE

        private void PopulateMapList()
        {
            foreach (var map in _mapListData.MapList)
            {
                foreach (var supportedGamemode in map.SupportedGamemodes)
                {
                    if (supportedGamemode.Value == _lobbyData.ChosenGamemode)
                    {
                        _lobbyData.AddMap(_lobbyData.ChosenGamemode, map.MapName);
                        break;
                    }
                }
            }
        }

        private IEnumerator WaitForBoltShutdown()
        {
            while (BoltNetwork.IsConnected)
            {
                yield return new WaitForEndOfFrame();
            }

            if (OnBoltShutdown != null)
            {
                OnBoltShutdown.Invoke();
            }
        }

        private IEnumerator WaitForConnectedAsServer()
        {
            while(!(BoltNetwork.IsConnected && BoltNetwork.IsServer))
            {
                yield return new WaitForEndOfFrame();
            }

            if (OnSwitchedToServer != null)
            {
                OnSwitchedToServer.Invoke();
            }
        }

        private IEnumerator WaitForConnectedAsClient()
        {
            while (!(BoltNetwork.IsConnected && BoltNetwork.IsClient))
            {
                yield return new WaitForEndOfFrame();
            }

            if (OnSwitchedToClient != null)
            {
                OnSwitchedToClient.Invoke();
            }
        }
    }
}
