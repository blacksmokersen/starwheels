using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Multiplayer
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class ServerSpawnGenerator : GlobalEventListener
    {
        private List<BoltEntity> _players = new List<BoltEntity>();
        private List<GameObject> _spawns = new List<GameObject>();
        private int _nbOfPlayersInGame = 2;
        private int _playersReady = 0;
        private bool _readyToAssignSpawns = false;

        // CORE

        // BOLT

        public override void SceneLoadLocalDone(string map)
        {
            _spawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.Spawn));
            BoltConsole.Write("Got Spawns");
        }

        public override void OnEvent(PlayerReady evnt)
        {
            BoltConsole.Write("Player Ready : " + evnt.Entity.networkId);
            _playersReady++;
            _players.Add(evnt.Entity);
            if(_playersReady == _nbOfPlayersInGame)
            {
                AssignSpawns();
            }
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

        // PRIVATE

        private void AssignSpawns()
        {
            BoltConsole.Write("Assigning spawns");
            foreach(var player in _players)
            {
                PlayerSpawn playerSpawn = PlayerSpawn.Create();
                playerSpawn.NetworkID = player.networkId;
                playerSpawn.SpawnPosition = GetSpawnPosition();
                playerSpawn.Send();
            }
        }
    }
}
