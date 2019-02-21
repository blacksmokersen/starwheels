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

        private List<TeamSpawn> _initialSpawns = new List<TeamSpawn>();
        private List<TeamSpawn> _respawns = new List<TeamSpawn>();
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
            var serverTeam = _serverPlayerSettings.ColorSettings.TeamEnum;
            Debug.Log("Server Team : " + serverTeam);
            _serverSpawn = GetInitialSpawnPosition(serverTeam);
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart, RoomInfoToken);
            myKart.transform.position = _serverSpawn.transform.position;
            myKart.transform.rotation = _serverSpawn.transform.rotation;
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
        }

        public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
        {
            var playerTeam = (Team)connection.UserData;
            AssignSpawn((int)connection.ConnectionId, playerTeam);
            IncreaseSpawnCount();
        }

        public override void OnEvent(RespawnRequest evnt)
        {
            var team = (Team)System.Enum.Parse(typeof(Team),evnt.Team);
            AssignSpawn(evnt.ConnectionID, team, true);
        }

        // PUBLIC

        // PRIVATE

        private void InitializeSpawns()
        {
            var spawns = FindObjectsOfType<TeamSpawn>();
            _initialSpawns = new List<TeamSpawn>(spawns);
            _respawns = new List<TeamSpawn>(spawns);
        }

        private void AssignSpawn(int connectionID, Team team, bool respawn = false)
        {
            GameObject spawn;

            if (respawn)
            {
                spawn = GetRespawnPosition(team);
            }
            else
            {
                spawn = GetInitialSpawnPosition(team);
            }

            PlayerSpawn playerSpawn = PlayerSpawn.Create();
            playerSpawn.ConnectionID = connectionID;
            playerSpawn.SpawnPosition = spawn.transform.position;
            playerSpawn.SpawnRotation = spawn.transform.rotation;
            playerSpawn.RoomToken = RoomInfoToken;
            playerSpawn.Send();
        }

        private GameObject GetInitialSpawnPosition(Team team)
        {
            return GetRandomSpawnFromList(team, _initialSpawns);
        }

        private GameObject GetRespawnPosition(Team team)
        {
            return GetRandomSpawnFromList(team, _respawns);
        }

        private GameObject GetRandomSpawnFromList(Team team, List<TeamSpawn> spawnList)
        {
            var validSpawns = new List<GameObject>();

            foreach (var spawn in spawnList)
            {
                if (spawn.Team == team)
                {
                    validSpawns.Add(spawn.gameObject);
                }
            }

            if(validSpawns.Count > 0)
            {
                return validSpawns[Random.Range(0, validSpawns.Count)];
            }
            else
            {
                return null;
            }
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
