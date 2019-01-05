using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Photon;

namespace Multiplayer
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class SpawnAssigner : GlobalEventListener
    {
        public RoomProtocolToken RoomInfoToken;

        private List<GameObject> _initialBlueSpawns = new List<GameObject>();
        private List<GameObject> _initialRedSpawns = new List<GameObject>();
        private List<GameObject> _blueSpawns = new List<GameObject>();
        private List<GameObject> _redSpawns = new List<GameObject>();
        private int _spawnsAssigned = 1;
        private GameObject _serverSpawn;

        private int _playersCount = -1;
        private bool _gameIsReady = false;
        private PlayerSettings _serverPlayerSettings;

        // CORE

        private void Awake()
        {
            _serverPlayerSettings = Resources.Load<PlayerSettings>("PlayerSettings");
        }

        // BOLT

        #if UNITY_EDITOR
        public override void BoltStartDone()
        {
            InitializeSpawns();
        }
        #endif

        public override void SceneLoadLocalDone(string map, IProtocolToken token)
        {
            InitializeSpawns();

            RoomInfoToken = (RoomProtocolToken)token;
            _playersCount = RoomInfoToken.PlayersCount;

            // Instantiate server kart
            var serverTeam = _serverPlayerSettings.TeamColor.GetTeam();
            _serverSpawn = GetInitialSpawnPosition(serverTeam);
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart, RoomInfoToken);
            myKart.transform.position = _serverSpawn.transform.position;
            myKart.transform.rotation = _serverSpawn.transform.rotation;
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
        }

        public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
        {
            var playerTeam = ((Color)connection.UserData).GetTeam();
            AssignSpawn((int)connection.ConnectionId, playerTeam);
            IncreaseSpawnCount();
        }

        public override void OnEvent(RespawnRequest evnt)
        {
            var team = evnt.Team.GetTeam();
            AssignSpawn(evnt.ConnectionID, team, true);
        }

        // PUBLIC

        // PRIVATE

        private void InitializeSpawns()
        {
            _redSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.RedSpawn));
            _initialRedSpawns = new List<GameObject>(_redSpawns);
            _blueSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.BlueSpawn));
            _initialBlueSpawns = new List<GameObject>(_blueSpawns);
        }

        private void AssignSpawn(int connectionID, Team team, bool respawn = false)
        {
            GameObject spawn;
            if (respawn) spawn = GetSpawnPosition(team);
            else spawn = GetInitialSpawnPosition(team);

            PlayerSpawn playerSpawn = PlayerSpawn.Create();
            playerSpawn.ConnectionID = connectionID;
            playerSpawn.SpawnPosition = spawn.transform.position;
            playerSpawn.SpawnRotation = spawn.transform.rotation;
            playerSpawn.RoomToken = RoomInfoToken;
            playerSpawn.Send();
        }

        private GameObject GetInitialSpawnPosition(Team team)
        {
            GameObject spawn = null;
            int randomIndex = 0;

            switch (team)
            {
                case Team.Blue:
                    randomIndex = Random.Range(0, _initialBlueSpawns.Count);
                    spawn = _initialBlueSpawns[randomIndex];
                    _initialBlueSpawns.Remove(spawn);
                    break;
                case Team.Red:
                    randomIndex = Random.Range(0, _initialRedSpawns.Count);
                    spawn = _initialRedSpawns[randomIndex];
                    _initialRedSpawns.Remove(spawn);
                    break;
            }
            return spawn;
        }

        private GameObject GetSpawnPosition(Team team)
        {
            GameObject spawnPosition = null;
            int randomIndex = 0;

            switch (team)
            {
                case Team.Blue:
                    randomIndex = Random.Range(0, _blueSpawns.Count);
                    spawnPosition = _blueSpawns[randomIndex];
                    break;
                case Team.Red:
                    randomIndex = Random.Range(0, _redSpawns.Count);
                    spawnPosition = _redSpawns[randomIndex];
                    break;
            }
            return spawnPosition;
        }

        private void IncreaseSpawnCount()
        {
            _spawnsAssigned++;
            if(_spawnsAssigned >= _playersCount)
            {
                _gameIsReady = true;
            }

            if (_gameIsReady)
            {
                // LAUNCH GAME OR COUNTDOWN
            }
        }
    }
}
