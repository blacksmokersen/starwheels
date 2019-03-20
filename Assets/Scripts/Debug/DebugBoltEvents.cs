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
            gameOverEvent.WinningTeam = Team.Blue.ToString();
            gameOverEvent.Send();
        }

        public void TriggerGameReadyEvent()
        {
            GameReady gameReadyEvent = GameReady.Create();
            gameReadyEvent.Send();
        }

        public void TriggerRedScoreIncreaseEvent()
        {
            ScoreIncreased scoreIncreasedEvent = ScoreIncreased.Create();
            scoreIncreasedEvent.Team = Team.Red.ToString();
            scoreIncreasedEvent.Score = int.Parse(_redScoreInput.text);
            scoreIncreasedEvent.Send();
        }

        public void TriggerBlueScoreIncreaseEvent()
        {
            ScoreIncreased scoreIncreasedEvent = ScoreIncreased.Create();
            scoreIncreasedEvent.Team = Team.Blue.ToString();
            scoreIncreasedEvent.Score = int.Parse(_blueScoreInput.text);
            scoreIncreasedEvent.Send();
        }

        public void TriggerPlayerHitEvent()
        {
            PlayerHit playerHitEvent = PlayerHit.Create();
            playerHitEvent.VictimEntity = SWExtensions.KartExtensions.GetKartWithID(SWMatchmaking.GetMyBoltId()).GetComponent<BoltEntity>();
            playerHitEvent.Send();
        }
    }
}
