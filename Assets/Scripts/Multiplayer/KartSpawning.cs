using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class KartSpawning : GlobalEventListener
    {
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

        public override void SceneLoadLocalDone(string map)
        {
            InstantiateKart();
            PlayerReady playerReady = PlayerReady.Create();
            playerReady.Entity = _me;
            playerReady.Send();
        }

        public override void OnEvent(PlayerSpawn evnt)
        {
            if(evnt.NetworkID == _me.networkId)
            {
                var playerInfo = (PlayerInfoToken)_me.attachToken;
                SpawnKart(evnt.SpawnPosition);
            }
        }

        // PRIVATE

        private void InstantiateKart()
        {
            if (!_kartInstantiated && BoltNetwork.isConnected)
            {
                _me = BoltNetwork.Instantiate(BoltPrefabs.Kart, new Vector3(0,0,0), Quaternion.identity);
                _kartInstantiated = true;
            }
        }

        private void SpawnKart(Vector3 spawnPoint)
        {
            _me.transform.position = spawnPoint;
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(_me);
        }
    }
}
