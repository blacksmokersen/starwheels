using GameModes;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Kart
{
    public class KartGameMode : BaseKartComponent
    {
        private int _score;

        private new void Awake()
        {
            base.Awake();
            kartEvents.OnKartDestroyed += DestroyKart;
            kartEvents.OnScoreChange();

            PhotonNetwork.LocalPlayer.SetScore(0);
        }

        public void DestroyKart()
        {
            switch (GameModeBase.ActualGameMode)
            {
                case GameMode.ClassicBattle:
                    ClassicBattle.OnKartDestroyed(PhotonNetwork.LocalPlayer.GetTeam());
                    break;
                case GameMode.BankRobbery:
                    break;
                case GameMode.GoldenTotem:
                    break;
                default:
                    break;
            }
        }

        public void IncreaseScore()
        {
            _score++;
            PhotonNetwork.LocalPlayer.SetScore(_score);
            PhotonView view = GetComponent<PhotonView>();
            view.RPC("RPCUpdateScore", RpcTarget.AllBuffered);
        }

        // PRIVATE

        [PunRPC]
        private void RPCUpdateScore()
        {
            KartEvents.Instance.OnScoreChange();
        }
    }
}
