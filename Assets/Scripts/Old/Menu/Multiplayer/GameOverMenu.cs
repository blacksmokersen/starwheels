using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using GameModes;

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
            replayButton.enabled = PhotonNetwork.IsMasterClient;
        }

        // PUBLIC

        public void SetWinnerTeam(PunTeams.Team team)
        {
            switch (team)
            {
                case PunTeams.Team.blue:
                    panelImage.color = Color.blue;
                    winnerTeamText.text = "Blue team wins !";
                    break;
                case PunTeams.Team.red:
                    panelImage.color = Color.red;
                    winnerTeamText.text = "Red team wins !";
                    break;
            }
        }

        // PRIVATE

        private void OnReplayButtonPressed()
        {
            GameModeEvents.Instance.OnGameReset();
        }
    }
}
