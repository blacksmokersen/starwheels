using UnityEngine;

namespace Gamemodes
{
    public class BattleServerRules : GamemodeBase
    {
        public IntVariable MaxScore;

        private GameSettings _gameSettings;

        // CORE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
        }

        // BOLT

        public override void OnEvent(OnAllPlayersInGame evnt)
        {
            if (_gameSettings.Gamemode == Constants.Gamemodes.Battle)
            {
                this.gameObject.SetActive(false);
            }
        }

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
