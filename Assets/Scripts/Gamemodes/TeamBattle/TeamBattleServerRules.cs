﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWExtensions;
using Multiplayer;

namespace Gamemodes
{
    public class TeamBattleServerRules : GamemodeBase
    {
        [SerializeField] private TeamBattleSettings _teamBattleSettings;

        private Dictionary<int, Coroutine> _jailCoroutines = new Dictionary<int, Coroutine>();
        private Dictionary<int, Team> _alivePlayers = new Dictionary<int, Team>();

        private TeamBattlePlayersObserver _teamBattlePlayersObserver;

        //CORE

        private void Awake()
        {
            _teamBattlePlayersObserver = GetComponent<TeamBattlePlayersObserver>();
        }

        //PRIVATE

        public void RefreshAlivePlayers()
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

        public void CheckWiningCondition()
        {
            int blueTeamNumbers = 0;
            int redTeamNumbers = 0;
            foreach (int player in _alivePlayers.Keys)
            {
                if(_alivePlayers[player] == Team.Blue)
                    blueTeamNumbers++;
                else if(_alivePlayers[player] == Team.Red)
                    redTeamNumbers++;
            }
            if(blueTeamNumbers == 0 || redTeamNumbers == 0)
            {
                //GAME OVER
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




    }
}
