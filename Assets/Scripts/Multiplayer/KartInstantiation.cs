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

        public override void BoltStartDone()
        {
            InstantiateKart(new Vector3(0, 0, 0));
        }

        // PRIVATE

        private IEnumerator WaitForSpawn()
        {
            while(SpawnPosition == null)
            {
                SpawnPosition = new Vector3(0,0,0);
                yield return null;
            }
            yield break;
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
