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
        [SerializeField] private PlayerSettings _playerSettings;

        public RoomProtocolToken RoomInfoToken;

        // private List<TeamSpawn> _initialSpawns = new List<TeamSpawn>();
        private Dictionary<TeamSpawn, Team> _initialSpawns = new Dictionary<TeamSpawn, Team>();
        private Dictionary<TeamSpawn, Team> _respawns = new Dictionary<TeamSpawn, Team>();
        //  private List<TeamSpawn> _respawns = new List<TeamSpawn>();
        private GameObject _serverSpawn;
        private TeamAssigner _teamAssigner;

        private int _spawnsAssigned = 0;
        private int _playersCount = -1;
        private bool _gameIsReady = false;
        private bool _gameIsStarted = false;
        private bool _countdownRoutineIsStarted = false;

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

                //Debug.LogError("RoomInfo : " + RoomInfoToken.RoomInfo + "   Gamemode : " + RoomInfoToken.Gamemode + "   PlayersCount : " + RoomInfoToken.PlayersCount);

                _playersCount = RoomInfoToken.PlayersCount;

                // Instantiate server kart
                var serverTeam = Team.None;


                if (_lobbySettings.GameSettings.Gamemode == "Battle")
                {
                    Debug.LogError("BATTLE GAMEMODE");
                    serverTeam = Team.None;

                    if (_playerSettings.Team == Team.None)
                    {
                        serverTeam = _teamAssigner.PickAvailableTeam();
                    }
                    else
                    {
                        serverTeam = _playerSettings.Team;
                    }
                }
                else
                {
                    Debug.LogError("FFA GAMEMODE");
                    serverTeam = _teamAssigner.PickAvailableTeam();
                }

                AssignSpawn(SWMatchmaking.GetMyBoltId(), serverTeam);
                _teamAssigner.AddPlayer(serverTeam, SWMatchmaking.GetMyBoltId());
            }
        }

        public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
        {
            if (BoltNetwork.IsServer)
            {
                Team playerTeam = Team.None;
                var joinToken = (JoinToken)connection.ConnectToken;

                if (_lobbySettings.GameSettings.Gamemode == "Battle")
                {
                    if (_lobbySettings.PlayersTeamDictionary.ContainsKey(joinToken.Nickname))
                    {
                        Debug.LogError("CORRESPONDANCE ON PLAYER : " + joinToken.Nickname);
                        playerTeam = _lobbySettings.PlayersTeamDictionary[joinToken.Nickname].ToTeam();
                    }
                    else
                    {
                        playerTeam = _teamAssigner.PickAvailableTeam();
                        Debug.LogError("NO CORRESPONDANCE ON PLAYER : " + joinToken.Nickname);
                    }
                }
                else
                {
                    playerTeam = _teamAssigner.PickAvailableTeam();
                }


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
                var leaveToken = (JoinToken)connection.ConnectToken;
                _lobbySettings.PlayersTeamDictionary.Remove(leaveToken.Nickname);
            }
        }

        #endregion

        // PUBLIC

        // PRIVATE

        private void InitializeSpawns()
        {
            var spawns = FindObjectsOfType<TeamSpawn>();

            foreach (TeamSpawn teamSpawns in spawns)
            {
                _initialSpawns.Add(teamSpawns, teamSpawns.Team);
            }

            foreach (TeamSpawn teamSpawns in spawns)
            {
                _respawns.Add(teamSpawns, teamSpawns.Team);
            }
        }

        private void AssignSpawn(int connectionID, Team team, bool respawn = false)
        {
            GameObject spawn;

            Debug.LogError("Id : " + connectionID + " TEAM  : " + team);

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
            playerSpawn.TeamEnum = (int)team;
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

        private GameObject GetRandomSpawnFromList(Team team, Dictionary<TeamSpawn, Team> spawnList)
        {
            var validSpawns = new List<GameObject>();

            foreach (var spawn in spawnList)
            {
                if (spawn.Value == team)
                {
                    validSpawns.Add(spawn.Key.gameObject);
                }
            }

            if (validSpawns.Count > 0)
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
                OnAllPlayersInGame onAllPlayersInGame = OnAllPlayersInGame.Create();
                if (_gameIsStarted)
                    onAllPlayersInGame.IsGameAlreadyStarted = true;
                else
                    onAllPlayersInGame.IsGameAlreadyStarted = false;

                onAllPlayersInGame.Send();

                if (!_gameIsStarted && !_countdownRoutineIsStarted)
                {
                    StartCoroutine(CountdownCoroutine());
                    _countdownRoutineIsStarted = true;
                }
            }
        }

        private IEnumerator CountdownCoroutine()
        {
            int remainingTime = _countdownSettings.Timer;
            // int floorTime = Mathf.FloorToInt(remainingTime);

            LobbyCountdown countdownEvent;

            if (_countdownSettings.Countdown)
            {
                while (remainingTime > 0)
                {
                    yield return new WaitForSeconds(1);
                    remainingTime--;

                    countdownEvent = LobbyCountdown.Create();
                    countdownEvent.Time = remainingTime;
                    countdownEvent.Send();

                    /*
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
                    */
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
