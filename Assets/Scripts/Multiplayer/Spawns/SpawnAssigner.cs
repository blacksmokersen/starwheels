using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Multiplayer
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class SpawnAssigner : GlobalEventListener
    {
        private List<GameObject> _initialBlueSpawns = new List<GameObject>();
        private List<GameObject> _initialRedSpawns = new List<GameObject>();
        private List<GameObject> _blueSpawns = new List<GameObject>();
        private List<GameObject> _redSpawns = new List<GameObject>();
        private int _playersCount = -1;
        private int _spawnsAssigned = 0;
        private PlayerSettings _serverPlayerSettings;

        // CORE

        private void Awake()
        {
            _serverPlayerSettings = Resources.Load<PlayerSettings>("PlayerSettings");
        }

        // BOLT

        public override void SceneLoadLocalDone(string map, IProtocolToken token)
        {
            _redSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.RedSpawn));
            _initialRedSpawns = _redSpawns;
            _blueSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.BlueSpawn));
            _initialBlueSpawns = _blueSpawns;

            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            var serverColor = Teams.TeamsColors.GetTeamFromColor(_serverPlayerSettings.Team);
            myKart.transform.position = GetInitialSpawnPosition(serverColor);
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);

            var roomToken = (Photon.RoomProtocolToken)token;
            if (System.Int32.TryParse(roomToken.RoomInfo, out _playersCount))
            {
                _spawnsAssigned++;
                Debug.Log("Players count : " + _playersCount);
            }
        }

        public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
        {
            var playerTeam = Teams.TeamsColors.GetTeamFromColor((Color)connection.UserData);
            AssignSpawn((int)connection.ConnectionId, playerTeam);
            _spawnsAssigned++;
        }

        public override void OnEvent(KartDestroyed evnt)
        {
            AssignSpawn(evnt.ConnectionID);
        }

        // PUBLIC

        // PRIVATE

        private void AssignSpawn(int connectionID, Team team = Team.None)
        {
            PlayerSpawn playerSpawn = PlayerSpawn.Create();
            playerSpawn.ConnectionID = connectionID;
            playerSpawn.SpawnPosition = GetInitialSpawnPosition(team);
            playerSpawn.Send();
        }

        private Vector3 GetInitialSpawnPosition(Team team)
        {
            if (_initialRedSpawns.Count > 0)
            {
                GameObject spawnPosition = null;
                int randomIndex = 0;

                switch (team)
                {
                    case Team.Blue:
                        randomIndex = Random.Range(0, _initialBlueSpawns.Count - 1);
                        spawnPosition = _initialBlueSpawns[randomIndex];
                        _initialBlueSpawns.Remove(spawnPosition);
                        break;
                    case Team.Red:
                        randomIndex = Random.Range(0, _initialRedSpawns.Count - 1);
                        spawnPosition = _initialRedSpawns[randomIndex];
                        _initialRedSpawns.Remove(spawnPosition);
                        break;
                }
                return spawnPosition.transform.position;
            }
            return Vector3.zero;
        }

        private Vector3 GetSpawnPosition(Team team)
        {
            GameObject spawnPosition = null;
            int randomIndex = 0;

            switch (team)
            {
                case Team.Blue:
                    randomIndex = Random.Range(0, _blueSpawns.Count - 1);
                    spawnPosition = _blueSpawns[randomIndex];
                    break;
                case Team.Red:
                    randomIndex = Random.Range(0, _redSpawns.Count - 1);
                    spawnPosition = _redSpawns[randomIndex];
                    break;
            }
            return spawnPosition.transform.position;
        }
    }
}
