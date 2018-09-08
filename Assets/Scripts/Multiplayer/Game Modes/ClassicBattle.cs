using UnityEngine;
using Controls;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

namespace GameModes
{
    public class ClassicBattle : GameModeBase
    {
        public static int MaxPlayersPerTeam;
        public static bool IsOver;
        public static PunTeams.Team WinnerTeam = PunTeams.Team.none;

        [SerializeField] private static GameObject _endGameMenu;

        private static int _redKartsAlive;
        private static int _blueKartsAlive;

        // CORE

        private void Awake()
        {
            CurrentGameMode = GameMode.ClassicBattle;

            _endGameMenu = Resources.Load<GameObject>(Constants.ClassicBattleEndMenu);
            _endGameMenu.SetActive(false);
        }

        private new void Start()
        {
            base.Start();
            InitializePlayerCount();
        }

        // PUBLIC

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

        // PRIVATE

        private static void InitializePlayerCount()
        {
            Player[] players = PhotonNetwork.PlayerList;

            foreach(Player player in players)
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

        private static void CheckIfOver()
        {
            if (_redKartsAlive <= 0)
            {
                IsOver = true;
                WinnerTeam = PunTeams.Team.blue;
            }
            else if (_blueKartsAlive <= 0)
            {
                IsOver = true;
                WinnerTeam = PunTeams.Team.red;
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
