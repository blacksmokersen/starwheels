using UnityEngine;
using Bolt;
using Photon.Lobby;

namespace Multiplayer
{
    public class KartSpawning : GlobalEventListener
    {
        // BOLT

        public override void OnEvent(PlayerSpawn evnt)
        {
            Debug.Log("Event for " + evnt.ConnectionID);
            InstantiateKart(evnt.SpawnPosition);
        }

        // PRIVATE

        private void InstantiateKart(Vector3 spawnPosition)
        {
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            myKart.transform.position = spawnPosition;
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart); // Set on Control Gained (network behaviour)
        }
    }
}
