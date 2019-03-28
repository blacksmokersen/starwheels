using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class InGameScoresPanelUpdater : GlobalEventListener
    {
        public Dictionary<int, PlayerInGameScoresEntry> PlayerScoreEntries;

        [Header("Entry Data")]
        [SerializeField] private PlayerInGameScoresEntry _prefab;

        // BOLT

        public override void Disconnected(BoltConnection connection)
        {
            DestroyEntryForPlayer((int)connection.ConnectionId);
        }

        // PUBLIC

        public void CreateEntryForPlayer(int id)
        {
            var entry = Instantiate(_prefab);
            PlayerScoreEntries.Add(id, entry);
        }

        public void DestroyEntryForPlayer(int id)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                Destroy(PlayerScoreEntries[id]);
            }
            else
            {
                Debug.LogError("Entry couldn't be destroyed since ID was not found.");
            }
        }

        public void UpdatePlayerKillCount(int id, int killCount)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                PlayerScoreEntries[id].UpdateKillCount(killCount);
            }
            else
            {
                Debug.LogError("Provided ID could be found in the players scores entries.");
            }
        }

        public void UpdatePlayerDeathCount(int id, int deathCount)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                PlayerScoreEntries[id].UpdateKillCount(deathCount);
            }
            else
            {
                Debug.LogError("Provided ID could be found in the players scores entries.");
            }
        }

        public void UpdatePlayerAbility(int id, int abilityIndex)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                PlayerScoreEntries[id].UpdateAbilityLogo(abilityIndex);
            }
            else
            {
                Debug.LogError("Provided ID could be found in the players scores entries.");
            }
        }
    }
}
