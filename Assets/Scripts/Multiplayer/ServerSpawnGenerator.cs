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
        private List<GameObject> _spawns = new List<GameObject>();

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
            Debug.Log("SCENELOADREMOTE!!");
            var lobbyPlayer = connection.GetLobbyPlayer();
            Debug.Log(lobbyPlayer == null);
            Debug.Log("Connection from : " + lobbyPlayer.state.Name);
            PlayerSpawn playerSpawn = PlayerSpawn.Create();
            playerSpawn.Nickname = lobbyPlayer.state.Name;
            playerSpawn.SpawnPosition = GetSpawnPosition();
            playerSpawn.Send();
            Debug.Log("MESSAGE SEEEENT");
        }

        public Vector3 GetSpawnPosition()
        {
            if (_spawns.Count > 0)
            {
                var spawnPosition = _spawns[0];
                _spawns.Remove(spawnPosition);
                return spawnPosition.transform.position;
            }
            return Vector3.zero;
        }
    }
}
