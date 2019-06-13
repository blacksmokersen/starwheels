using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using Bolt;
using UdpKit;
using System.Collections;
using System;

namespace SW.Matchmaking
{
    public class LobbyMaker : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("Events")]
        public UnityEvent OnLobbyCreated;

        [Header("DebugMode")]
        [SerializeField] private ServerDebugMode _serverDebugMode;
        public bool DebugModEnabled = false;

        // BOLT

        public override void BoltStartBegin()
        {
            if (BoltNetwork.IsServer)
            {
                Debug.Log("[BOLT] Registering tokens.");
                BoltNetwork.RegisterTokenClass<Multiplayer.RoomProtocolToken>();
                BoltNetwork.RegisterTokenClass<LobbyToken>();
                BoltNetwork.RegisterTokenClass<JoinToken>();
            }
        }

        // PUBLIC

        public void CreateLobby()
        {
            SWMatchmaking.SetRegion((int)udpkit.platform.photon.PhotonRegion.Regions.BEST_REGION);
            SWMatchmaking.CreateLobby();
            StartCoroutine(WaitForLobbyCreated());
        }

        // PRIVATE

        private void SetLobbyData()
        {
            if (DebugModEnabled)
            {
                _lobbyData.ServerName = _serverDebugMode.GetHostServerName();
            }
            else
            {
                _lobbyData.SetRandomName();
            }
            _lobbyData.GameStarted = false;
            _lobbyData.CanBeJoined = true;
            _lobbyData.SetRandomGamemode();
            _lobbyData.SetRandomMap();

            VerifyLobbyDataSanity();
            SWMatchmaking.SetLobbyData(_lobbyData);
        }

        private void VerifyLobbyDataSanity()
        {
            Assert.IsNotNull(_lobbyData.ServerName, "Server cannont be null.");
            Assert.IsNotNull(_lobbyData.ChosenMapName, "MapName cannot be null.");
            Assert.IsNotNull(_lobbyData.ChosenGamemode, "Gamemode cannot be null.");
            Assert.AreNotEqual(_lobbyData.MaxPlayers, 0, "MaxPlayers cannot be equal to 0.");
        }


        private IEnumerator WaitForLobbyCreated()
        {
            var timer = 0f;
            bool timerExceedeed = false;

            while (!BoltNetwork.IsServer && !timerExceedeed)
            {
                timer += Time.deltaTime;
                if (timer > 10f)
                {
                    timerExceedeed = true;
                }
                yield return new WaitForEndOfFrame();
            }

            if (BoltNetwork.IsServer)
            {
                SetLobbyData();
                if (OnLobbyCreated != null)
                {
                    OnLobbyCreated.Invoke();
                }
                Debug.Log("[BOLT] Lobby successfully created.");
            }

            else if (timerExceedeed)
            {
                Debug.LogWarning("[BOLT] Took too long to connect as client.");
            }
        }
    }
}
