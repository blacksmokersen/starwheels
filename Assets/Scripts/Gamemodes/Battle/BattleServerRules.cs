namespace Gamemodes
{
    public class BattleServerRules : GameModeBase
    {
        public IntVariable MaxScore;

        // BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            if (BoltNetwork.IsServer && evnt.KillerID != evnt.VictimID)
            {
                IncreaseScore(evnt.KillerTeam.ToTeam(), 1);
                SendScoreIncreasedEvent(evnt.KillerTeam.ToTeam());
                CheckScore();
            }
        }

        // PRIVATE

        private void CheckScore()
        {
            foreach (var entry in scores)
            {
                if (scores[entry.Key] >= MaxScore.Value)
                {
                    WinnerTeam = entry.Key;
                    SendWinningTeamEvent(entry.Key);
                }
            }
        }
    }
}
