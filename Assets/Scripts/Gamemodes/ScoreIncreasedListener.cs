using UnityEngine;
using Bolt;
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
            switch (evnt.Team.ToTeam())
            {
                case Team.Blue:
                    _blueScoreText.text = "" + evnt.Score;
                    break;
                case Team.Red:
                    _redScoreText.text = "" + evnt.Score;
                    break;
                default:
                    Debug.LogWarning("Unknown team.");
                    break;
            }
        }
    }
}
