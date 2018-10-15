using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Multiplayer
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class ServerSpawnGenerator : GlobalEventListener
    {
        private List<GameObject> _spawns;
        private bool _readyToAssignSpawns = false;

        // CORE

        // BOLT

        public override void SceneLoadLocalDone(string map)
        {
            if (BoltNetwork.isServer)
            {
                _spawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.Spawn));
                StartCoroutine(WaitForPlayers());
            }
        }

        /*
        public override void OnEvent()
        {

        }
        */

        public Transform GetSpawnPosition()
        {
            if (BoltNetwork.isServer)
            {
                if (_spawns.Count > 0)
                {
                    var spawnPosition = _spawns[0];
                    _spawns.Remove(spawnPosition);
                    return spawnPosition.transform;
                }
            }
            return null;
        }

        // PRIVATE

        private IEnumerator WaitForPlayers()
        {
            while (Application.isPlaying)
            {
                yield return null;
            }
            _readyToAssignSpawns = true;
        }
    }
}
