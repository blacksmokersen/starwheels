using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Photon;
using System.Collections;
using Multiplayer.Teams;

namespace Multiplayer
{
    [RequireComponent(typeof(TeamAssigner))]
    public class SpawnAssigner : GlobalEventListener
    {
        [Header("Settings")]
        [SerializeField] private FloatVariable _countdownSeconds;
        [SerializeField] private PlayerSettings _serverPlayerSettings;

        public RoomProtocolToken RoomInfoToken;

        private List<GameObject> _initialBlueSpawns = new List<GameObject>();
        private List<GameObject> _initialRedSpawns = new List<GameObject>();
        private List<GameObject> _blueSpawns = new List<GameObject>();
        private List<GameObject> _redSpawns = new List<GameObject>();
        private GameObject _serverSpawn;
        private TeamAssigner _teamAssigner;

        private int _spawnsAssigned = 0;
        private int _playersCount = -1;
        private bool _gameIsReady = false;

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
                Team playerTeam = Teams.TeamsColors.BlueColor.GetTeam();
                if (connection.UserData != null)
                {
                    playerTeam = ((Color)connection.UserData).GetTeam();
                }
                else
                {
                    playerTeam = _teamAssigner.PickAvailableTeam();
                }
                AssignSpawn((int)connection.ConnectionId, playerTeam);
                _teamAssigner.AddPlayer(playerTeam, (int)connection.ConnectionId);
            }
        }

        public override void OnEvent(RespawnRequest evnt)
        {
            if (BoltNetwork.IsServer)
            {
                var team = evnt.Team.GetTeam();
                AssignSpawn(evnt.ConnectionID, team, true);
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
            playerSpawn.TeamEnum = team.ToString();
            playerSpawn.Send();

            IncreaseSpawnCount();
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
            float remainingTime = _countdownSeconds.Value;
            int floorTime = Mathf.FloorToInt(remainingTime);

            LobbyCountdown countdownEvent;

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

            countdownEvent = LobbyCountdown.Create(GlobalTargets.Everyone);
            countdownEvent.Time = 0;
            countdownEvent.Send();

            GameReady gameReadyEvent = GameReady.Create(GlobalTargets.Everyone);
            gameReadyEvent.Send();
        }
    }
}
