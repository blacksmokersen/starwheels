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
            gameOverEvent.WinningTeam = TeamsColors.BlueColor;
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
            scoreIncreasedEvent.Team = TeamsColors.RedColor;
            scoreIncreasedEvent.Score = int.Parse(_redScoreInput.text);
            scoreIncreasedEvent.Send();
        }

        public void TriggerBlueScoreIncreaseEvent()
        {
            ScoreIncreased scoreIncreasedEvent = ScoreIncreased.Create();
            scoreIncreasedEvent.Team = TeamsColors.BlueColor;
            scoreIncreasedEvent.Score = int.Parse(_blueScoreInput.text);
            scoreIncreasedEvent.Send();
        }
    }
}
