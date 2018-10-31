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

        private List<GameObject> _initialSpawns = new List<GameObject>();
        private List<GameObject> _spawns = new List<GameObject>();
        private SpawnMode _spawnMode = SpawnMode.Random;

        // CORE

        // BOLT

        public override void SceneLoadLocalDone(string map)
        {
            _initialSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.Spawn));
            _spawns = _initialSpawns;

            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            myKart.transform.position = GetInitialSpawnPosition();
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
        }

        public override void SceneLoadRemoteDone(BoltConnection connection)
        {
            PlayerSpawn playerSpawn = PlayerSpawn.Create();
            playerSpawn.ConnectionID = (int) connection.ConnectionId;
            playerSpawn.SpawnPosition = GetInitialSpawnPosition();
            playerSpawn.Send();
        }

        public override void OnEvent(KartDestroyed evnt)
        {
            RespawnPlayer(evnt.ConnectionID);
        }

        // PUBLIC

        // PRIVATE

        private void RespawnPlayer(int connectionID)
        {
            PlayerSpawn playerSpawn = PlayerSpawn.Create();
            playerSpawn.ConnectionID = connectionID;
            playerSpawn.SpawnPosition = GetSpawnPosition();
            playerSpawn.Send();
        }

        private Vector3 GetInitialSpawnPosition()
        {
            if (_initialSpawns.Count > 0)
            {
                GameObject spawnPosition = _initialSpawns[0];

                if (_spawnMode == SpawnMode.Random)
                {
                    int randomIndex = Random.Range(0, _initialSpawns.Count - 1);
                    spawnPosition = _initialSpawns[randomIndex];
                }

                _initialSpawns.Remove(spawnPosition);
                return spawnPosition.transform.position;
            }
            return Vector3.zero;
        }

        private Vector3 GetSpawnPosition()
        {
            if (_initialSpawns.Count > 0)
            {
                GameObject spawnPosition = _initialSpawns[0];

                if (_spawnMode == SpawnMode.Random)
                {
                    int randomIndex = Random.Range(0, _initialSpawns.Count - 1);
                    spawnPosition = _spawns[randomIndex];
                }

                return spawnPosition.transform.position;
            }
            return Vector3.zero;
        }
    }
}
