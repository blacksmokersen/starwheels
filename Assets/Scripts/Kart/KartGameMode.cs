using GameModes;

namespace Kart
{
    public class KartGameMode : BaseKartComponent
    {
        private new void Awake()
        {
            base.Awake();
            kartEvents.OnKartDestroyed += DestroyKart;
        }

        public void DestroyKart()
        {
            switch (GameModeBase.ActualGameMode)
            {
                case GameMode.ClassicBattle:
                    ClassicBattle.OnKartDestroyed(PhotonNetwork.player.GetTeam());
                    break;
                case GameMode.BankRobbery:
                    break;
                case GameMode.GoldenTotem:
                    break;
                default:
                    break;
            }
        }
    }
}
