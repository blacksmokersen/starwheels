using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Multiplayer
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class SpawnAssigner : GlobalEventListener
    {
        public IProtocolToken RoomInfoToken;

        private List<GameObject> _initialBlueSpawns = new List<GameObject>();
        private List<GameObject> _initialRedSpawns = new List<GameObject>();
        private List<GameObject> _blueSpawns = new List<GameObject>();
        private List<GameObject> _redSpawns = new List<GameObject>();
        private int _playersCount = -1;
        private int _spawnsAssigned = 0;
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

            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart);
            var serverColor = Teams.TeamsColors.GetTeamFromColor(_serverPlayerSettings.Team);
            myKart.transform.position = new Vector3(0, 10, 0);
            /*
            var spawn = GetInitialSpawnPosition(serverColor);
            myKart.transform.position = spawn.transform.position;
            myKart.transform.rotation = spawn.transform.rotation;
            */
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);

            var roomToken = (Photon.RoomProtocolToken)token;
            RoomInfoToken = roomToken;

            if (System.Int32.TryParse(roomToken.RoomInfo, out _playersCount))
            {
                IncreaseSpawnCount();
            }
        }

        public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
        {
            var playerTeam = Teams.TeamsColors.GetTeamFromColor((Color)connection.UserData);
            AssignSpawn((int)connection.ConnectionId, playerTeam);
            IncreaseSpawnCount();
        }

        public override void OnEvent(KartDestroyed evnt)
        {
            var team = Teams.TeamsColors.GetTeamFromColor(evnt.Team);
            AssignSpawn(evnt.ConnectionID, team, true);
        }

        // PUBLIC

        // PRIVATE

        private void InitializeSpawns()
        {
            _redSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.RedSpawn));
            _initialRedSpawns = _redSpawns;
            _blueSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag(Constants.Tag.BlueSpawn));
            _initialBlueSpawns = _blueSpawns;
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
        }
    }
}
