using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class SpawnEventListener : GlobalEventListener
    {
        [SerializeField] private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            if (!BoltNetwork.isConnected) // Used for In-Editor tests
            {
                BoltLauncher.StartServer();
            }
        }

        // BOLT

        public override void BoltStartDone() // Used for In-Editor tests
        {
            InstantiateKart(transform.position, Quaternion.identity); // Scene specific position
        }

        public override void OnEvent(PlayerSpawn evnt)
        {
            if(evnt.ConnectionID == _playerSettings.ConnectionID)
            {
                InstantiateKart(evnt.SpawnPosition, evnt.SpawnRotation);
            }
        }

        // PRIVATE

        private void InstantiateKart(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            myKart.transform.position = spawnPosition;
            myKart.transform.rotation = spawnRotation;
        }
    }
}
