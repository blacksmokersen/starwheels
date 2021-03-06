﻿using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Multiplayer.Teams
{
    public class TeamAssigner : GlobalEventListener
    {
        private GameSettings _gameSettings;
        private Dictionary<Team, List<int>> _teamsPlayers = new Dictionary<Team, List<int>>();

        // AWAKE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
            RegisterAvailableTeams();
        }

        // BOLT

        public override void Disconnected(BoltConnection connection)
        {
            var playerID = (int)connection.ConnectionId;
            Team playerTeam = Team.None;
            bool found = false;

            foreach (var pair in _teamsPlayers)
            {
                if (found)
                {
                    break;
                }
                if (pair.Value.Contains(playerID))
                {
                    playerTeam = pair.Key;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                _teamsPlayers[playerTeam].Remove(playerID);
            }

            CheckRemainingTeams();
        }

        // PUBLIC

        public Team PickAvailableTeam()
        {
            int leastPlayerCount = int.MaxValue;
            Team availableTeam = Team.Blue;

            foreach (var pair in _teamsPlayers)
            {
                if (pair.Value.Count < leastPlayerCount)
                {
                    leastPlayerCount = pair.Value.Count;
                    availableTeam = pair.Key;
                }
            }
            return availableTeam;
        }

        public void AddPlayer(Team team, int playerID)
        {
            if (!PlayerAlreadyInATeam(playerID) && _teamsPlayers.ContainsKey(team))
            {
                _teamsPlayers[team].Add(playerID);
            }
        }

        // PRIVATE

        private void RegisterAvailableTeams()
        {
            foreach (var team in _gameSettings.TeamsListSettings.TeamsList)
            {
                _teamsPlayers.Add(team.TeamEnum, new List<int>());
            }
        }

        private bool PlayerAlreadyInATeam(int playerID)
        {
            foreach (var entry in _teamsPlayers)
            {
                if (entry.Value.Contains(playerID))
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckRemainingTeams()
        {
            Team remainingTeam = Team.None;
            int remainingTeamCount = 0;

            foreach (var entry in _teamsPlayers)
            {
                if (entry.Value.Count > 0)
                {
                    remainingTeam = entry.Key;
                    remainingTeamCount++;
                }
            }

            if (remainingTeamCount == 1)
            {
                GameOver gameOverEvent = GameOver.Create();
                gameOverEvent.WinningTeam = (int)remainingTeam;
                gameOverEvent.Send();
            }
        }
    }
}
