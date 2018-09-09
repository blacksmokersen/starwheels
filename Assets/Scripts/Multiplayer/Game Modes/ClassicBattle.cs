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

        private static GameObject _endGameMenu;
        private static int _redKartsAlive;
        private static int _blueKartsAlive;

        // CORE

        private void Awake()
        {
            CurrentGameMode = GameMode.ClassicBattle;

            _endGameMenu = Instantiate(Resources.Load<GameObject>(Constants.Prefab.EndGameMenu));
            _endGameMenu.SetActive(false);
        }

        private new void Start()
        {
            base.Start();
            InitializePlayerCount();
            Debug.Log("Red players : " + _redKartsAlive);
            Debug.Log("Blue players : " + _blueKartsAlive);
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
                Debug.Log("Blue wins !");
                IsOver = true;
                WinnerTeam = PunTeams.Team.blue;
                EndGame();
            }
            else if (_blueKartsAlive <= 0)
            {
                Debug.Log("Red wins !");
                IsOver = true;
                WinnerTeam = PunTeams.Team.red;
                EndGame();
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
