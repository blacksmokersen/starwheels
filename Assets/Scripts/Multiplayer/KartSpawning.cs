using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class KartSpawning : GlobalEventListener
    {
        [SerializeField] private PlayerSettings _playerSettings;

        // CORE

        // BOLT

        public override void OnEvent(PlayerSpawn evnt)
        {
            if(evnt.ConnectionID == _playerSettings.ConnectionID)
            {
                InstantiateKart(evnt.SpawnPosition);
            }
        }

        // PRIVATE

        private void InstantiateKart(Vector3 spawnPosition)
        {
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            myKart.transform.position = spawnPosition;
        }
    }
}
