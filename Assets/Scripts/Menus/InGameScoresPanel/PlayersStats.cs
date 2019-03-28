using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Menu.InGameScores
{
    public class PlayerStats
    {
        // BATTLE
        public int KillCount;
        public int DeathCount;

        // TOTEM
        public int IndividualGoals;
        public int Passes;
        public int Saves;
    }

    public class PlayersStats : GlobalEventListener
    {
        public Dictionary<int, PlayerStats> AllPlayersStats;

        [Header("Events")]
        public DoubleIntEvent OnPlayerKillCountUpdated;
        public DoubleIntEvent OnPlayerDeathCountUpdated;

        // BOLT SPECIFIC EVENTS

        public override void Connected(BoltConnection connection)
        {
            CreateEntryForPlayerID((int)connection.ConnectionId);
        }

        public override void Disconnected(BoltConnection connection)
        {
            RemoveEntryForPlayerID((int)connection.ConnectionId);
        }

        // GAMEPLAY EVENTS

        public override void OnEvent(PlayerHit evnt)
        {
            UpdatePlayerKillCount(evnt.KillerID); // change to killer
            UpdatePlayerDeathCount(evnt.VictimID);
        }

        // PUBLIC

        public void CreateEntryForPlayerID(int id)
        {
            AllPlayersStats.Add(id, new PlayerStats());
        }

        public void RemoveEntryForPlayerID(int id)
        {
            AllPlayersStats.Remove(id);
        }

        public void UpdatePlayerKillCount(int id)
        {
            AllPlayersStats[id].KillCount++;
            OnPlayerKillCountUpdated.Invoke(id, AllPlayersStats[id].KillCount);
        }

        public void UpdatePlayerDeathCount(int id)
        {
            AllPlayersStats[id].DeathCount++;
            OnPlayerDeathCountUpdated.Invoke(id, AllPlayersStats[id].DeathCount);
        }
    }
}
