using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer.Teams
{
    public class TeamAssigner : MonoBehaviour
    {
        private Dictionary<Team, List<int>> _teamsPlayers = new Dictionary<Team, List<int>>();

        // AWAKE

        private void Awake()
        {
            _teamsPlayers.Add(Team.Blue, new List<int>());
            _teamsPlayers.Add(Team.Red, new List<int>());
        }

        // PUBLIC

        public Team PickAvailableTeam()
        {
            var bluePlayersCount = _teamsPlayers[Team.Blue].Count;
            var redPlayersCount = _teamsPlayers[Team.Red].Count;

            if (bluePlayersCount > redPlayersCount)
            {
                return Team.Red;
            }
            else if (bluePlayersCount < redPlayersCount)
            {
                return Team.Blue;
            }
            else
            {
                return Team.Blue;
            }
        }

        public void AddPlayer(Team team, int playerID)
        {
            if (!PlayerAlreadyInATeam(playerID) && _teamsPlayers.ContainsKey(team))
            {
                _teamsPlayers[team].Add(playerID);
            }
        }

        // PRIVATE

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
