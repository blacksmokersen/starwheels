using UnityEngine;
using Controls;

namespace GameModes
{
    public class ClassicBattle : GameMode
    {
        public static int MaxPlayersPerTeam;
        public static bool IsOver;
        public static PunTeams.Team WinnerTeam = PunTeams.Team.none;

        [SerializeField] private static GameObject _endGameMenu;

        private static int _redKartsAlive;
        private static int _blueKartsAlive;

        private void Awake()
        {
            _endGameMenu = Resources.Load<GameObject>(Constants.ClassicBattleEndMenu);
            _endGameMenu.SetActive(false);
        }

        private new void Start()
        {
            base.Start();
            InitializePlayerCount();
        }

        public static void OnKartDestroyed(PunTeams.Team team)
        {
            switch (team)
            {
                case PunTeams.Team.blue:
                    _blueKartsAlive--;
                    break;
                case PunTeams.Team.red:
                    _redKartsAlive--;
                    break;
                default:
                    break;
            }
            CheckIfOver();
        }

        private static void CheckIfOver()
        {
            if(_redKartsAlive <= 0)
            {
                IsOver = true;
                WinnerTeam = PunTeams.Team.blue;
            }
            else if(_blueKartsAlive <= 0)
            {
                IsOver = true;
                WinnerTeam = PunTeams.Team.red;
            }
        }

        private static void InitializePlayerCount()
        {
            PhotonPlayer[] players = PhotonNetwork.playerList;
            foreach(PhotonPlayer player in players)
            {
                switch (player.GetTeam())
                {
                    case PunTeams.Team.blue:
                        _blueKartsAlive++;
                        break;
                    case PunTeams.Team.red:
                        _redKartsAlive++;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void EndGame()
        {
            var playerInputsList = FindObjectsOfType<PlayerInputs>();
            foreach(PlayerInputs playerInputs in playerInputsList)
            {
                playerInputs.Enabled = false;
            }
            _endGameMenu.SetActive(true);
        }
    }
}
