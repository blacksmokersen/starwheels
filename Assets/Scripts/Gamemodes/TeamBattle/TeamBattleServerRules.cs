using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWExtensions;
using Multiplayer;

namespace Gamemodes
{
    public class TeamBattleServerRules : GamemodeBase
    {
        public TeamBattleSettings TeamBattleSettings;

        [SerializeField] Transform _redTeamJailPosition;
        [SerializeField] Transform _blueTeamJailPosition;

        [SerializeField] Transform _deathStoragePosition;

        private Dictionary<int, Coroutine> _jailCoroutines = new Dictionary<int, Coroutine>();
        private Dictionary<int, Team> _alivePlayers = new Dictionary<int, Team>();

        private TeamBattlePlayersObserver _teamBattlePlayersObserver;

        //CORE

        private void Awake()
        {
            _teamBattlePlayersObserver = GetComponent<TeamBattlePlayersObserver>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                RefreshAlivePlayers();
                foreach (int player in _alivePlayers.Keys)
                {
                    Debug.LogError("Player ID : " + player + " Team : " + _alivePlayers[player]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                GameObject playerKart = KartExtensions.GetMyKart();

                PermanentDeath permanentdeath = PermanentDeath.Create();
                permanentdeath.PlayerEntity = playerKart.GetComponent<BoltEntity>();
                permanentdeath.TimeBeforeDeath = 0;
                permanentdeath.PlayerID = playerKart.GetComponent<PlayerInfo>().OwnerID;
                permanentdeath.PlayerTeam = playerKart.GetComponent<PlayerInfo>().Team.ToString();
                permanentdeath.PlayerTeam = playerKart.GetComponent<PlayerInfo>().Nickname;
                permanentdeath.Send();
            }
        }

        //PUBLIC

        public void RefreshAlivePlayers()
        {
            if (BoltNetwork.IsServer)
            {
                _alivePlayers.Clear();
                foreach (int player in _teamBattlePlayersObserver.GetAlivePlayers())
                {
                    if (!_alivePlayers.ContainsKey(player))
                    {
                        AddPlayerEntry(player, KartExtensions.GetKartWithID(player).GetComponent<PlayerInfo>().Team);
                    }
                }
                CheckWiningCondition();
            }
        }

        public void CheckWiningCondition()
        {
            int blueTeamNumbers = 0;
            int redTeamNumbers = 0;
            foreach (int player in _alivePlayers.Keys)
            {
                if (_alivePlayers[player] == Team.Blue)
                    blueTeamNumbers++;
                else if (_alivePlayers[player] == Team.Red)
                    redTeamNumbers++;
            }
            if (blueTeamNumbers == 0)
            {
                SendWinningTeamEvent(Team.Red);
            }
            else if (redTeamNumbers == 0)
            {
                SendWinningTeamEvent(Team.Blue);
            }
        }

        public void AddPlayerEntry(int playerID, Team team)
        {
            _alivePlayers.Add(playerID, team);
        }

        public void RemovePlayerEntry(int playerID)
        {
            _alivePlayers.Remove(playerID);
        }

        public void SendPlayerToJail(int playerID)
        {
            GameObject kart = KartExtensions.GetKartWithID(playerID);

            if (kart.GetComponent<PlayerInfo>().Team == Team.Red)
            {
                kart.transform.position = _blueTeamJailPosition.position;
                kart.transform.rotation = _blueTeamJailPosition.rotation;
                Debug.LogError("Forced player to jail BLUE : " + playerID);
            }
            else if (kart.GetComponent<PlayerInfo>().Team == Team.Blue)
            {
                kart.transform.position = _redTeamJailPosition.position;
                kart.transform.rotation = _redTeamJailPosition.rotation;
                Debug.LogError("Forced player to jail RED: " + playerID);
            }
        }

        public void SendPlayerToDeathStorage(int playerID)
        {
            GameObject kart = KartExtensions.GetKartWithID(playerID);

            kart.transform.position = _deathStoragePosition.position;
            kart.transform.rotation = _deathStoragePosition.rotation;
            Debug.LogError("Forced player to death storage : " + playerID);
        }
    }
}
