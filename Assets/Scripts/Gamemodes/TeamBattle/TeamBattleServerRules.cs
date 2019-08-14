using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWExtensions;
using Multiplayer;

namespace Gamemodes
{
    public class TeamBattleServerRules : GamemodeBase
    {
        private Dictionary<int, Coroutine> _jailCoroutines = new Dictionary<int, Coroutine>();
        private Dictionary<int, Team> _alivePlayers = new Dictionary<int, Team>();

        private TeamBattlePlayersObserver _teamBattlePlayersObserver;

        //CORE

        private void Awake()
        {
            _teamBattlePlayersObserver = GetComponent<TeamBattlePlayersObserver>();
        }

        //PRIVATE

        public void SetAlivePlayers()
        {
            foreach (int player in _teamBattlePlayersObserver.GetAlivePlayers())
            {
                if (!_alivePlayers.ContainsKey(player))
                {
                    AddPlayerEntry(player, KartExtensions.GetMyKart().GetComponent<PlayerInfo>().Team);
                }
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
