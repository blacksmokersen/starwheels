using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Menu.InGameScores
{
    public class TeamStats
    {
        public Team Team;
        public int KillCount = 0;
        public int DeathCount = 0;
    }

    [DisallowMultipleComponent]
    public class TeamsStats : GlobalEventListener
    {
        public Dictionary<Team, TeamStats> AllTeamsStats = new Dictionary<Team, TeamStats>();

        [Header("Events")]
        public TeamEvent OnTeamKillCountUpdated;
        public TeamEvent OnTeamDeathCountUpdated;

        [Header("References")]
        [SerializeField] private PlayersStats _playersStats;

        // BOLT SPECIFIC EVENTS

        public override void OnEvent(PlayerReady evnt)
        {
            if (!AllTeamsStats.ContainsKey(evnt.Team.ToTeam()))
            {
                CreateEntryForTeam(evnt.Team.ToTeam());
            }
            else
            {
                Debug.Log("Could not add the player stats entry since it already exists.");
            }
        }

        public override void Disconnected(BoltConnection connection)
        {
            if (AllTeamsStats.ContainsKey(Team.Any))
            {
                // TODO
            }
            else
            {
                Debug.Log("Could not remove the player stats entry since his ID was not found.");
            }
        }

        // GAMEPLAY EVENTS

        public override void OnEvent(PlayerStatUpdate evnt)
        {
            if (evnt.StatName == Constants.PlayerStats.DeathCountName)
            {
                UpdateTeamDeathCount(evnt.Team.ToTeam());
            }
            else if (evnt.StatName == Constants.PlayerStats.KillCountName)
            {
                UpdateTeamKillCount(evnt.Team.ToTeam());
            }
        }

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

        public void UpdateTeamDeathCount(Team team)
        {
            if (AllTeamsStats.ContainsKey(team))
            {
                var deathCount = GetTeamDeathCount(team);
                AllTeamsStats[team].DeathCount = deathCount;

                if (OnTeamDeathCountUpdated != null)
                {
                    OnTeamDeathCountUpdated.Invoke(team);
                }
            }
        }

        public void UpdateTeamKillCount(Team team)
        {
            if (AllTeamsStats.ContainsKey(team))
            {
                var killCount = GetTeamKillCount(team);
                AllTeamsStats[team].KillCount = killCount;

                if (OnTeamKillCountUpdated != null)
                {
                    OnTeamKillCountUpdated.Invoke(team);
                }
            }
        }

        // PRIVATE

        private int GetTeamDeathCount(Team team)
        {
            int teamDeathCount = 0;
            foreach (var pair in _playersStats.AllPlayersStats)
            {
                if (pair.Value.Team == team)
                {
                    teamDeathCount += pair.Value.DeathCount;
                }
            }
            return teamDeathCount;
        }

        private int GetTeamKillCount(Team team)
        {
            int teamKillCount = 0;
            foreach(var pair in _playersStats.AllPlayersStats)
            {
                if (pair.Value.Team == team)
                {
                    teamKillCount += pair.Value.KillCount;
                }
            }
            Debug.Log("Team kill count : " + teamKillCount);
            return teamKillCount;
        }
    }
}
