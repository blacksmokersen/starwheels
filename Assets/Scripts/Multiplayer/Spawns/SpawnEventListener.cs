using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class SpawnEventListener : GlobalEventListener
    {
        [SerializeField] private PlayerSettings _playerSettings;

        private Photon.RoomProtocolToken _roomToken;

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
            InstantiateKart(transform.position, transform.rotation); // Scene specific position
        }

        public override void SceneLoadLocalBegin(string scene, IProtocolToken token)
        {
            _roomToken = (Photon.RoomProtocolToken) token;
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
            GameObject myKart;

            if (_roomToken != null)
            {
                myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart, _roomToken);
            }
            else
            {
                Debug.LogError("RoomToken not set.");
                myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            }

            myKart.transform.position = spawnPosition;
            myKart.transform.rotation = spawnRotation;
        }
    }
}
