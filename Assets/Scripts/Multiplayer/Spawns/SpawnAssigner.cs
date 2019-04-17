using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Photon;
using System.Collections;
using Multiplayer.Teams;
using SW.Matchmaking;

namespace Multiplayer
{
    [RequireComponent(typeof(TeamAssigner))]
    public class SpawnAssigner : GlobalEventListener
    {
        [Header("Settings")]
        [SerializeField] private CountdownSettings _countdownSettings;
        [SerializeField] private LobbyData _lobbySettings;

        public RoomProtocolToken RoomInfoToken;

        private List<TeamSpawn> _initialSpawns = new List<TeamSpawn>();
        private List<TeamSpawn> _respawns = new List<TeamSpawn>();
        private GameObject _serverSpawn;
        private TeamAssigner _teamAssigner;

        private int _spawnsAssigned = 0;
        private int _playersCount = -1;
        private bool _gameIsReady = false;
        private bool _gameIsStarted = false;

        // CORE

        private void Awake()
        {
            _teamAssigner = GetComponent<TeamAssigner>();
        }

        // BOLT

#if UNITY_EDITOR
        public override void BoltStartDone()
        {
            if (BoltNetwork.IsServer)
            {
                InitializeSpawns();

                GameReady gameReadyEvent = GameReady.Create(GlobalTargets.Everyone);
                gameReadyEvent.Send();
            }
        }
#endif

        public override void SceneLoadLocalDone(string map, IProtocolToken token)
        {
            if (BoltNetwork.IsServer)
            {
                InitializeSpawns();

                RoomInfoToken = (RoomProtocolToken)token;
                _playersCount = RoomInfoToken.PlayersCount;

                // Instantiate server kart
                var serverTeam = _teamAssigner.PickAvailableTeam();
                AssignSpawn(SWMatchmaking.GetMyBoltId(), serverTeam);
                _teamAssigner.AddPlayer(serverTeam, SWMatchmaking.GetMyBoltId());
            }
        }

        public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
        {
            if (BoltNetwork.IsServer)
            {
                Team playerTeam = _teamAssigner.PickAvailableTeam();
                AssignSpawn((int)connection.ConnectionId, playerTeam);
                _teamAssigner.AddPlayer(playerTeam, (int)connection.ConnectionId);
                IncreaseSpawnCount();
            }
        }

        public override void OnEvent(RespawnRequest evnt)
        {
            if (BoltNetwork.IsServer)
            {
                AssignSpawn(evnt.ConnectionID, evnt.Team.ToTeam(), true);
            }
        }

        #region Clients Callbacks

        public override void Connected(BoltConnection connection)
        {
            if (BoltNetwork.IsServer)
            {
                _playersCount++;
            }
        }

        public override void Disconnected(BoltConnection connection)
        {
            if (BoltNetwork.IsServer)
            {
                _playersCount--;
            }
        }

        #endregion

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
            playerSpawn.TeamEnum = team.ToString();
            playerSpawn.GameStarted = _gameIsStarted;
            playerSpawn.Send();

            if (!respawn)
            {
                IncreaseSpawnCount();
            }
        }

        private GameObject GetInitialSpawnPosition(Team team)
        {
            if (_lobbySettings.ChosenGamemode == Constants.Gamemodes.FFA)
            {
                return GetRandomSpawnFromList(Team.Any, _initialSpawns);
            }
            else
            {
                return GetRandomSpawnFromList(team, _initialSpawns);
            }
        }

        private GameObject GetRespawnPosition(Team team)
        {
            if (_lobbySettings.ChosenGamemode == Constants.Gamemodes.FFA)
            {
                return GetRandomSpawnFromList(Team.Any, _respawns);
            }
            else
            {
                return GetRandomSpawnFromList(team, _respawns);
            }
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
            if (_spawnsAssigned >= _playersCount)
            {
                _gameIsReady = true;
            }

            if (_gameIsReady)
            {
                StartCoroutine(CountdownCoroutine());
            }
        }

        private IEnumerator CountdownCoroutine()
        {
            float remainingTime = _countdownSettings.Timer;
            int floorTime = Mathf.FloorToInt(remainingTime);

            LobbyCountdown countdownEvent;

            if (_countdownSettings.Countdown)
            {
                while (remainingTime > 0)
                {
                    remainingTime -= Time.deltaTime;
                    int newFloorTime = Mathf.FloorToInt(remainingTime);

                    if (newFloorTime != floorTime)
                    {
                        floorTime = newFloorTime;

                        countdownEvent = LobbyCountdown.Create(GlobalTargets.Everyone);
                        countdownEvent.Time = floorTime;
                        countdownEvent.Send();
                    }
                    yield return null;
                }
            }

            countdownEvent = LobbyCountdown.Create(GlobalTargets.Everyone);
            countdownEvent.Time = 0;
            countdownEvent.Send();

            GameReady gameReadyEvent = GameReady.Create(GlobalTargets.Everyone);
            gameReadyEvent.Send();

            _gameIsStarted = true;
        }
    }
}
