using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using Bolt;
using UdpKit;

namespace SW.Matchmaking
{
    public class LobbyMaker : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;
        [SerializeField] private SessionData _sessionData;

        [Header("Events")]
        public UnityEvent OnConnectedAsServer;

        [Header("DebugMode")]
        [SerializeField] private ServerDebugMode _serverDebugMode;

        [HideInInspector] public bool DebugModEnabled = false;

        // BOLT

        public override void BoltStartBegin()
        {
            if (BoltNetwork.IsServer)
            {
                Debug.Log("Registering tokens...");
                BoltNetwork.RegisterTokenClass<Multiplayer.RoomProtocolToken>();
                BoltNetwork.RegisterTokenClass<LobbyToken>();
            }
        }

        public override void BoltStartDone()
        {
            if (BoltNetwork.IsServer)
            {
                if (DebugModEnabled)
                {
                    _lobbyData.ServerName = _serverDebugMode.GetHostServerName();
                }
                else
                {
                    _lobbyData.SetRandomName();
                }
                _lobbyData.CanBeJoined = true;
                _lobbyData.SetRandomGamemode();
                _lobbyData.SetRandomMap();
                SWMatchmaking.SetLobbyData(_lobbyData);

                OnConnectedAsServer.Invoke();
                Debug.Log("Bolt now running as server.");
            }
        }

        public override void SessionCreated(UdpSession session)
        {
            _sessionData.MySession = session;
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            _sessionData.MySession = null;
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
            Assert.IsNotNull(_lobbyData.ServerName, "Server cannont be null.");
            Assert.IsNotNull(_lobbyData.ChosenMapName, "MapName cannot be null.");
            Assert.IsNotNull(_lobbyData.ChosenGamemode, "Gamemode cannot be null.");
            Assert.AreNotEqual(_lobbyData.MaxPlayers, 0, "MaxPlayers cannot be equal to 0.");
        }
    }
}
