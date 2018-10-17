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
            myKart.GetState<IKartState>().Team = LobbyPhotonPlayer.localPlayer.playerColor;
            myKart.GetState<IKartState>().Nickname = LobbyPhotonPlayer.localPlayer.playerName;
            myKart.transform.position = GetSpawnPosition();
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
        }

        public override void SceneLoadRemoteDone(BoltConnection connection)
        {
            var player = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            var lobbyPlayer = LobbyPlayerList._instance.GetPlayer(connection);
            player.GetState<IKartState>().Team = lobbyPlayer.playerColor;
            player.GetState<IKartState>().Nickname = lobbyPlayer.playerName;
            player.AssignControl(connection);
            player.ReleaseControl();
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
