using UnityEngine;
using Bolt;
using Photon.Lobby;

namespace Multiplayer
{
    public class KartSpawning : GlobalEventListener
    {
        [SerializeField] private PlayerSettingsSO _playerSettings;

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
         //   FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart); // Set on Control Gained (network behaviour)
        }
    }
}
