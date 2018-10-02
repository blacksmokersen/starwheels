using UnityEngine;
using CameraUtils;
using GameModes;
using HUD;

namespace Kart
{
    public class KartGameMode : MonoBehaviour
    {
        private int _score;

        private void Awake()
        {
            //PhotonNetwork.LocalPlayer.SetScore(0);

            //kartEvents.OnKartDestroyed += DestroyKart;
        }

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
            /*if (photonView.IsMine)
            {
                FindObjectOfType<SpectatorControls>().Enabled = true;
                FindObjectOfType<CameraPlayerSwitch>().SetCameraToRandomPlayer();
                kartHub.DestroyKart();
                PhotonView photonView = GetComponent<PhotonView>();
                photonView.RPC("RPCClassicBattleDestroy", RpcTarget.AllBuffered);
            }*/
        }

        #endregion

        #region Score

        public void IncreaseScore()
        {
            _score++;
            //PhotonNetwork.LocalPlayer.SetScore(_score);
            //photonView.RPC("RPCUpdateScore", RpcTarget.AllBuffered);
        }

        // PRIVATE


        #endregion
    }
}
