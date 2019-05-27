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
        public UnityEvent OnBoltStartDone;
        public UnityEvent OnBoltShutdown;

        [Header("Other Events")]
        public UnityEvent OnSwitchedToServer;
        public UnityEvent OnSwitchedToClient;
        public UnityEvent OnGameCreated;

        // BOLT

        public override void BoltStartDone()
        {
            if (OnBoltStartDone != null)
            {
                OnBoltStartDone.Invoke();
            }
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            if (OnBoltShutdown != null)
            {
                OnBoltShutdown.Invoke();
            }
        }

        // PUBLIC

        public void ShutdownBolt()
        {
            BoltLauncher.Shutdown();
        }

        public void SwitchToServer()
        {
            BoltLauncher.StartServer();

            if (OnSwitchedToServer != null)
            {
                OnSwitchedToServer.Invoke();
            }
        }

        public void SwitchToClient()
        {
            BoltLauncher.StartClient();

            if (OnSwitchedToClient != null)
            {
                OnSwitchedToClient.Invoke();
            }
        }

        public void CreateRandomGame()
        {
            Debug.Log("Creating random game");
            PopulateMapList();
            _lobbyData.SetRandomMap();
            SWMatchmaking.SetLobbyData(_lobbyData);
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
                if (map.ExclusiveGameMode.Value == _lobbyData.ChosenGamemode)
                {
                    _lobbyData.AddMap(_lobbyData.ChosenGamemode, map.MapName);
                }
            }
        }
    }
}
