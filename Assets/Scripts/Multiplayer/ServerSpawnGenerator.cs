using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Photon.Lobby;

namespace Multiplayer
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class ServerSpawnGenerator : GlobalEventListener
    {
        public enum SpawnMode { Random, Deterministic }

        private List<GameObject> _spawns = new List<GameObject>();
        private SpawnMode _spawnMode = SpawnMode.Random;

        // CORE

        // BOLT

        public override void SceneLoadLocalDone(string map)
        {
            _spawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.Spawn));
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            myKart.transform.position = GetSpawnPosition();
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
        }

        public override void SceneLoadRemoteDone(BoltConnection connection)
        {
            PlayerSpawn playerSpawn = PlayerSpawn.Create();
            playerSpawn.ConnectionID = (int) connection.ConnectionId;
            playerSpawn.SpawnPosition = GetSpawnPosition();
            playerSpawn.Send();
        }

        public Vector3 GetSpawnPosition()
        {
            if (_spawns.Count > 0)
            {
                GameObject spawnPosition = _spawns[0];

                if (_spawnMode == SpawnMode.Random)
                {
                    int randomIndex = Random.Range(0, _spawns.Count - 1);
                    spawnPosition = _spawns[randomIndex];
                }

                _spawns.Remove(spawnPosition);
                return spawnPosition.transform.position;
            }
            return Vector3.zero;
        }
    }
}
