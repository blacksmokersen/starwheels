using UnityEngine;
using CameraUtils;
using Multiplayer.Teams;
using Bolt;

namespace Gamemodes
{
    public class KartGameMode : GlobalEventListener
    {
        [Header("Events")]
        public TeamEvent OnGameEnd;

        private GameObject _endGameMenu;

        // CORE

        private void Awake()
        {
            _endGameMenu = MonoBehaviour.Instantiate(Resources.Load<GameObject>(Constants.Resources.EndGameMenu));
            _endGameMenu.SetActive(false);
        }

        // BOLT

        public override void OnEvent(GameOver evnt)
        {
            _endGameMenu.SetActive(true);
            _endGameMenu.GetComponent<Menu.GameOverMenu>().SetWinnerTeam(evnt.WinningTeam.ToTeam());

            OnGameEnd.Invoke(evnt.WinningTeam.ToTeam());
        }

        // PRIVATE

        private void ClassicBattleDestroy()
        {
            FindObjectOfType<SpectatorControls>().Enabled = true;
            FindObjectOfType<CameraPlayerSwitch>().SetCameraToRandomPlayer();
        }
    }
}
