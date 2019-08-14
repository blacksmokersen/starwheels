using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Menu.InGameScores
{
    public class PlayerStats
    {
        // GENERICS
        public string Name;
        public Team Team;

        // BATTLE
        public int KillCount = 0;
        public int DeathCount = 0;

        // TEAM BATTLE

        public int LifeCount = 0;
        public float JailMaxDuration = 0;
        public int RescueFriendsCount = 0;

        // TOTEM
        public int IndividualGoals = 0;
        public int Passes = 0;
        public int Saves = 0;
    }

    public class PlayersStats : GlobalEventListener
    {
        public Dictionary<int, PlayerStats> AllPlayersStats = new Dictionary<int, PlayerStats>();

        [Header("Player Events")]
        public DoubleIntEvent OnPlayerKillCountUpdated;
        public DoubleIntEvent OnPlayerDeathCountUpdated;
        public DoubleIntEvent OnPlayerAbilityUpdated;

        [Header("Team Events")]
        public TeamEvent OnTeamMemberKillCountUpdated;
        public TeamEvent OnTeamMemberDeathCountUpdated;

        // BOLT SPECIFIC EVENTS

        public override void OnEvent(PlayerReady evnt)
        {
            if (!AllPlayersStats.ContainsKey(evnt.PlayerID))
            {
                CreateEntryForPlayerID(evnt.PlayerID, evnt.Nickname, evnt.Team.ToTeam());
            }
            else
            {
                Debug.Log("Could not add the player stats entry since it already exists.");
            }
        }

        public override void SceneLoadRemoteDone(BoltConnection connection)
        {
            if (BoltNetwork.IsServer)
            {
                SendStatsToPlayer((int)connection.ConnectionId);
            }
        }

        public override void Disconnected(BoltConnection connection)
        {
            if (AllPlayersStats.ContainsKey((int)connection.ConnectionId))
            {
                RemoveEntryForPlayerID((int)connection.ConnectionId);
            }
            else
            {
                Debug.Log("Could not remove the player stats entry since his ID was not found.");
            }
        }

        // GAMEPLAY EVENTS

        public override void OnEvent(PlayerHit evnt)
        {
            if (BoltNetwork.IsServer)
            {
                if (evnt.VictimID != evnt.KillerID)
                {
                    PlayerStatUpdate playerKillCountUpdate = PlayerStatUpdate.Create();
                    playerKillCountUpdate.StatName = Constants.PlayerStats.KillCountName;
                    playerKillCountUpdate.PlayerID = evnt.KillerID;
                    playerKillCountUpdate.Team = evnt.KillerTeam;
                    playerKillCountUpdate.StatValue = AllPlayersStats[evnt.KillerID].KillCount + 1;
                    playerKillCountUpdate.Send();
                }

                PlayerStatUpdate playerDeathCountUpdate = PlayerStatUpdate.Create();
                playerDeathCountUpdate.StatName = Constants.PlayerStats.DeathCountName;
                playerDeathCountUpdate.PlayerID = evnt.VictimID;
                playerDeathCountUpdate.Team = evnt.VictimTeam;
                playerDeathCountUpdate.StatValue = AllPlayersStats[evnt.VictimID].DeathCount + 1;
                playerDeathCountUpdate.Send();
            }
        }

        public override void OnEvent(OnUpdateAbility evnt)
        {
            if (BoltNetwork.IsServer)
            {
                PlayerStatUpdate playerAbilityUpdate = PlayerStatUpdate.Create();
                playerAbilityUpdate.StatName = Constants.PlayerStats.Ability;
                playerAbilityUpdate.PlayerID = evnt.PlayerID;
                playerAbilityUpdate.StatValue = evnt.AbilityIndex;
                playerAbilityUpdate.Send();
            }
        }

        public override void OnEvent(PlayerStatUpdate evnt)
        {
            if (evnt.StatName == Constants.PlayerStats.DeathCountName)
            {
                UpdatePlayerDeathCount(evnt.PlayerID, evnt.StatValue);
            }
            if (evnt.StatName == Constants.PlayerStats.KillCountName)
            {
                UpdatePlayerKillCount(evnt.PlayerID, evnt.StatValue);
            }
            if (evnt.StatName == Constants.PlayerStats.Ability)
            {
                UpdatePlayerAbility(evnt.PlayerID, evnt.StatValue);
            }
        }

        // PUBLIC

        public void CreateEntryForPlayerID(int id, string nickname, Team team)
        {
            var newStats = new PlayerStats()
            {
                Name = nickname,
                Team = team
            };
            AllPlayersStats.Add(id, newStats);
        }

        public int GetPlayerRank(int id)
        {
            var rank = AllPlayersStats.Count;
            var playerStats = AllPlayersStats[id];

            foreach (var pair in AllPlayersStats)
            {
                if (pair.Key != id && playerStats.KillCount > pair.Value.KillCount)
                {
                    rank = Mathf.Max(1, rank);
                }
            }
            return rank;
        }

        public void RemoveEntryForPlayerID(int id)
        {
            AllPlayersStats.Remove(id);
        }

        public void UpdatePlayerKillCount(int id, int count)
        {
            AllPlayersStats[id].KillCount = count;

            OnPlayerKillCountUpdated.Invoke(id, count);
            OnTeamMemberKillCountUpdated.Invoke(AllPlayersStats[id].Team);
        }

        public void UpdatePlayerDeathCount(int id, int count)
        {
            AllPlayersStats[id].DeathCount = count;

            OnPlayerDeathCountUpdated.Invoke(id, count);
            OnTeamMemberDeathCountUpdated.Invoke(AllPlayersStats[id].Team);
        }

        public void UpdatePlayerAbility(int id, int abilityIndex)
        {
            OnPlayerAbilityUpdated.Invoke(id, abilityIndex);
        }

        // PRIVATE

        private void SendStatsToPlayer(int playerID)
        {
            foreach (var playerStat in AllPlayersStats)
            {
                PlayerAllStats playerAllStats = PlayerAllStats.Create();
                playerAllStats.PlayerID = playerStat.Key;
                playerAllStats.SteamID = "" + Steamworks.SteamUser.GetSteamID().m_SteamID;
                playerAllStats.Name = playerStat.Value.Name;
                playerAllStats.Team = (int) playerStat.Value.Team;
                playerAllStats.TargetPlayerID = playerID;
                playerAllStats.KillCount = playerStat.Value.KillCount;
                playerAllStats.DeathCount = playerStat.Value.DeathCount;
                playerAllStats.Send();
            }
        }
    }
}
