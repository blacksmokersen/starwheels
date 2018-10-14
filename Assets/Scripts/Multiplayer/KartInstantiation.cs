using System.Collections;
using UnityEngine;

namespace Multiplayer
{
    public class KartInstantiation : Bolt.GlobalEventListener
    {
        public Vector3 SpawnPosition;

        private bool _kartInstantiated = false;

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

        }

        // PRIVATE

        private IEnumerator WaitForSpawn()
        {
            while(SpawnPosition == null)
            {
                SpawnPosition =
                yield return null;
            }
        }

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
