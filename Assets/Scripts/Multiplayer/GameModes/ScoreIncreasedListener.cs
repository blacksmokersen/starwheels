using UnityEngine;
using Bolt;
using Multiplayer.Teams;
using TMPro;

namespace Gamemodes
{
    public class ScoreIncreasedListener : GlobalEventListener
    {
        [SerializeField] private TextMeshProUGUI _blueScoreText;
        [SerializeField] private TextMeshProUGUI _redScoreText;

        // BOLT

        public override void OnEvent(ScoreIncreased evnt)
        {
            var team = evnt.Team.GetTeam();

            switch (team)
            {
                case Team.Blue:
                    _blueScoreText.text = "" + evnt.Score;
                    break;
                case Team.Red:
                    _redScoreText.text = "" + evnt.Score;
                    break;
                default:
                    Debug.LogError("Unknown team.");
                    break;
            }
        }
    }
}
