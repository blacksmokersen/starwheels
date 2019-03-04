using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using Bolt;

namespace SW.Matchmaking
{
    public class LobbyMaker : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("Events")]
        public UnityEvent OnConnectedAsServer;

        // BOLT

        public override void BoltStartBegin()
        {
            if (BoltNetwork.IsServer)
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
            if (BoltNetwork.IsServer)
            {
                Debug.Log("Bolt now running as server.");
                _lobbyData.SetRandomGamemode();
                _lobbyData.SetRandomMap();
                SWMatchmaking.SetLobbyData(_lobbyData);

                OnConnectedAsServer.Invoke();
            }
        }

        // PUBLIC

        public void CreateLobby()
        {
            VerifyLobbyDataSanity();
            SWMatchmaking.CreateLobby();
        }

        // PRIVATE

        private void VerifyLobbyDataSanity()
        {
            Assert.IsNotNull(_lobbyData.ServerName);
            Assert.IsNotNull(_lobbyData.ChosenMapName);
            Assert.IsNotNull(_lobbyData.ChosenGamemode);
            Assert.AreNotEqual(_lobbyData.MaxPlayers, 0);
        }
    }
}
