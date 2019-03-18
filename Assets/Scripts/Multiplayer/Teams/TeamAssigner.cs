using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer.Teams
{
    public class TeamAssigner : MonoBehaviour
    {
        private GameSettings _gameSettings;
        private Dictionary<Team, List<int>> _teamsPlayers = new Dictionary<Team, List<int>>();

        // AWAKE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
            RegisterAvailableTeams();
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
    }
}
