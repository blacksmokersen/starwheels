using CameraUtils;
using GameModes;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace Kart
{
    public class KartGameMode : BaseKartComponent
    {
        private int _score;

        private new void Awake()
        {
            base.Awake();
            PhotonNetwork.LocalPlayer.SetScore(0);

            kartEvents.OnKartDestroyed += DestroyKart;
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
            if (photonView.IsMine)
            {
                ClassicBattle.OnKartDestroyed(PhotonNetwork.LocalPlayer.GetTeam());
                FindObjectOfType<SpectatorControls>().Enabled = true;
                FindObjectOfType<CameraPlayerSwitch>().SetCameraToRandomPlayer();
                kartHub.DestroyKart();
            }
        }
        #endregion

        #region Score

        public void IncreaseScore()
        {
            _score++;
            PhotonNetwork.LocalPlayer.SetScore(_score);
            photonView.RPC("RPCUpdateScore", RpcTarget.AllBuffered);
        }

        // PRIVATE

        [PunRPC]
        private void RPCUpdateScore()
        {
            KartEvents.Instance.OnScoreChange();
        }

        #endregion
    }
}
