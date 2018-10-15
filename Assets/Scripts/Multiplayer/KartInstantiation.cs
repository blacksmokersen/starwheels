using System.Collections;
using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class KartInstantiation : GlobalEventListener
    {
        public Vector3 SpawnPosition;

        private bool _kartInstantiated = false;
        private BoltEntity _me;

        // CORE

        private void Awake()
        {
            if (!BoltNetwork.isConnected)
            {
                BoltLauncher.StartServer();
                Debug.Log("Starting bolt !");
            }
        }

        // BOLT

        public override void OnEvent(PlayerSpawn evnt)
        {
            if(evnt.NetworkID == _me.networkId)
            {
                InstantiateKart(evnt.SpawnPosition);
            }
        }

        public override void BoltStartDone()
        {
            PlayerReady playerReady = PlayerReady.Create();
            playerReady.Entity = _me;
            playerReady.Send();
        }

        // PRIVATE

        private void InstantiateKart(Vector3 spawnPoint)
        {
            if (!_kartInstantiated && BoltNetwork.isConnected)
            {
                var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart, spawnPoint, Quaternion.identity);
                FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
                _kartInstantiated = true;
            }
        }
    }
}
