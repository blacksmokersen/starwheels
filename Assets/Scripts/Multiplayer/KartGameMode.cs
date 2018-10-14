using UnityEngine;
using CameraUtils;
using GameModes;
using HUD;

namespace Kart
{
    public class KartGameMode : MonoBehaviour
    {
        private int _score;

        public void DestroyKart()
        {
            switch (GameModeBase.CurrentGameMode)
            {
                case GameMode.ClassicBattle:
                    ClassicBattleDestroy();
                    break;
                case GameMode.BankRobbery:
                    break;
                case GameMode.GoldenTotem:
                    break;
                default:
                    break;
            }
        }

        #region Destroy Functions
        private void ClassicBattleDestroy()
        {
            FindObjectOfType<SpectatorControls>().Enabled = true;
            FindObjectOfType<CameraPlayerSwitch>().SetCameraToRandomPlayer();
        }

        #endregion

        #region Score

        public void IncreaseScore()
        {
            _score++;
        }

        // PRIVATE


        #endregion
    }
}
