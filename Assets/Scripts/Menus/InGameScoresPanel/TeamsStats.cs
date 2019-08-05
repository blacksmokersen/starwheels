using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Menu.InGameScores
{
    public class TeamStats
    {
        public Team Team;
        public int KillCount = 0;
        public int DeathCount = 0;
    }

    public class TeamsStats : GlobalEventListener
    {
        public Dictionary<Team, TeamStats> AllTeamsStats = new Dictionary<Team, TeamStats>();

        [Header("Events")]
        public TeamIntEvent OnTeamKillCountUpdated;
        public TeamIntEvent OnTeamDeathCountUpdated;

        // BOLT

                // Possibilité de faire un event séparé TeamStatUpdate de la même façon que PlayerStatUpdate

        // PUBLIC

        public void CreateEntryForTeam(Team team)
        {
            var newStats = new TeamStats()
            {
                Team = team,
                KillCount = 0,
                DeathCount = 0
            };
            AllTeamsStats.Add(team, newStats);
        }

        public int GetTeamRank(Team team)
        {
            var rank = AllTeamsStats.Count;
            var playerStats = AllTeamsStats[team];

            foreach (var pair in AllTeamsStats)
            {
                if (pair.Key != team && playerStats.KillCount > pair.Value.KillCount)
                {
                    rank = Mathf.Max(1, rank - 1);
                }
            }
            return rank;
        }

        public void RemoveEntryForTeam(Team team)
        {
            AllTeamsStats.Remove(team);
        }

        public void UpdateTeamDeathCount(Team team, int deathCount)
        {
            if (AllTeamsStats.ContainsKey(team))
            {
                AllTeamsStats[team].DeathCount = deathCount;
                OnTeamDeathCountUpdated.Invoke(team, deathCount);
            }
        }

        public void UpdateTeamKillCount(Team team, int killCount)
        {
            if (AllTeamsStats.ContainsKey(team))
            {
                AllTeamsStats[team].KillCount = killCount;
                OnTeamKillCountUpdated.Invoke(team, killCount);
            }
        }
    }
}
