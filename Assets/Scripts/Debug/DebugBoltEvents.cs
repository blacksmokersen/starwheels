using UnityEngine;
using Multiplayer.Teams;
using TMPro;

namespace SW.DebugUtils
{
    public class DebugBoltEvents : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_InputField _redScoreInput;
        [SerializeField] private TMP_InputField _blueScoreInput;

        public void TriggerGameOverEvent()
        {
            GameOver gameOverEvent = GameOver.Create();
            gameOverEvent.WinningTeam = (int)Team.Blue;
            gameOverEvent.Send();
        }

        public void TriggerGameReadyEvent()
        {
            GameReady gameReadyEvent = GameReady.Create();
            gameReadyEvent.Send();
        }

        public void LobbyCoutndownEvent()
        {
            LobbyCountdown countdownEvent = LobbyCountdown.Create();
            countdownEvent.Time = 0;
            countdownEvent.Send();
        }

        public void TriggerRedScoreIncreaseEvent()
        {
            ScoreIncreased scoreIncreasedEvent = ScoreIncreased.Create();
            scoreIncreasedEvent.Team = (int)Team.Red;
            scoreIncreasedEvent.Score = int.Parse(_redScoreInput.text);
            scoreIncreasedEvent.Send();
        }

        public void TriggerBlueScoreIncreaseEvent()
        {
            ScoreIncreased scoreIncreasedEvent = ScoreIncreased.Create();
            scoreIncreasedEvent.Team = (int)Team.Blue;
            scoreIncreasedEvent.Score = int.Parse(_blueScoreInput.text);
            scoreIncreasedEvent.Send();
        }

        public void TriggerPlayerHitEvent()
        {
            PlayerHit playerHitEvent = PlayerHit.Create();
            playerHitEvent.VictimEntity = SWExtensions.KartExtensions.GetKartWithID(SWMatchmaking.GetMyBoltId()).GetComponent<BoltEntity>();
            playerHitEvent.Send();
        }

        public void TriggerPlayerQuitEvent()
        {
            PlayerQuit playerQuitEvent = PlayerQuit.Create();
            playerQuitEvent.PlayerID = SWMatchmaking.GetMyBoltId();
            playerQuitEvent.Send();
        }
    }
}
