using UnityEngine;
using Multiplayer;

namespace Gamemodes
{
    public class TeamBattleServerRules : GameModeBase
    {
        private int _maxScore = 15;

        // BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            IncreaseScore(evnt.KillerTeam.ToTeam(), 1);
            SendScoreIncreasedEvent(evnt.KillerTeam.ToTeam());
        }

        // PRIVATE

        private void CheckScore()
        {
            foreach (var entry in scores)
            {
                if (scores[entry.Key] > _maxScore)
                {
                    WinnerTeam = entry.Key;
                    SendWinningTeamEvent(entry.Key);
                }
            }
        }
    }
}
