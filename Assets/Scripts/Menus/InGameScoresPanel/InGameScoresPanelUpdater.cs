using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class InGameScoresPanelUpdater : GlobalEventListener
    {
        public Dictionary<int, InGameScoresEntry> PlayerScoreEntries;

        // BOLT SPECIFIC EVENTS

        public override void Connected(BoltConnection connection)
        {

        }

        public override void Disconnected(BoltConnection connection)
        {

        }

        // GAMEPLAY EVENTS

        public override void OnEvent(GameReady evnt)
        {

        }

        public override void OnEvent(PlayerHit evnt)
        {

        }

        public override void OnEvent(GameOver evnt)
        {

        }

        // PRIVATE

        private void UpdatePlayerScore(int id, int score)
        {
            if (PlayerScoreEntries.ContainsKey(id))
            {
                PlayerScoreEntries[id].UpdateScore(score);
            }
            else
            {
                Debug.LogError("Provided ID could be found in the players scores entries.");
            }
        }

        private void UpdatePlayerAbility(int id, int abilityIndex)
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
