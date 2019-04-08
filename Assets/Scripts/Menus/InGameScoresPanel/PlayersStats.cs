using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Menu.InGameScores
{
    public class PlayerStats
    {
        // BATTLE
        public int KillCount = 0;
        public int DeathCount = 0;

        // TOTEM
        public int IndividualGoals = 0;
        public int Passes = 0;
        public int Saves = 0;
    }

    public class PlayersStats : GlobalEventListener
    {
        public Dictionary<int, PlayerStats> AllPlayersStats = new Dictionary<int, PlayerStats>();

        [Header("Events")]
        public DoubleIntEvent OnPlayerKillCountUpdated;
        public DoubleIntEvent OnPlayerDeathCountUpdated;

        // BOLT SPECIFIC EVENTS

        public override void OnEvent(PlayerReady evnt)
        {
            if (!AllPlayersStats.ContainsKey(evnt.PlayerID))
            {
                CreateEntryForPlayerID(evnt.PlayerID);
            }
            else
            {
                Debug.Log("Could not add the player stats entry since it already exists.");
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
            if (evnt.KillerID != evnt.VictimID) // This is not a self kill
            {
                UpdatePlayerKillCount(evnt.KillerID);
            }
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
            if (AllPlayersStats.ContainsKey(id))
            {
                AllPlayersStats[id].KillCount += 1;
                OnPlayerKillCountUpdated.Invoke(id, AllPlayersStats[id].KillCount);
            }
            else
            {
                Debug.LogError("Could not find the appopriate ID in the stats.");
            }
        }

        public void UpdatePlayerDeathCount(int id)
        {
            AllPlayersStats[id].DeathCount += 1;
            OnPlayerDeathCountUpdated.Invoke(id, AllPlayersStats[id].DeathCount);
        }
    }
}
