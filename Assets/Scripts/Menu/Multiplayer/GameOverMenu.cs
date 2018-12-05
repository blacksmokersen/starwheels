using UnityEngine;
using UnityEngine.UI;
using Multiplayer.Teams;

namespace Menu
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Image panelImage;
        [SerializeField] private Text winnerTeamText;
        [SerializeField] private Button replayButton;

        private void Awake()
        {
            replayButton.onClick.AddListener(OnReplayButtonPressed);
        }

        private void OnEnable()
        {
            replayButton.gameObject.SetActive(BoltNetwork.isServer);
        }

        // PUBLIC

        public void SetWinnerTeam(Team team)
        {
            switch (team)
            {
                case Team.Blue:
                    winnerTeamText.text = "Blue team wins !";
                    break;
                case Team.Red:
                    winnerTeamText.text = "Red team wins !";
                    break;
            }
            panelImage.color = team.GetColor();
        }

        // PRIVATE

        private void OnReplayButtonPressed()
        {
            FindObjectOfType<LevelManager>().ResetLevel();
        }
    }
}
