using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Multiplayer;

namespace GameModes
{
    public class ClassicBattle : GameModeBase
    {
        [HideInInspector] public static ClassicBattle Instance;

        [Header("GameMode Settings")]
        public int MaxPlayersPerTeam;
        public bool IsOver;
        public Team WinnerTeam = Team.None;

        private GameObject _endGameMenu;
        private int _redKartsAlive;
        private int _blueKartsAlive;

        // CORE

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            CurrentGameMode = GameMode.ClassicBattle;

            _endGameMenu = Instantiate(Resources.Load<GameObject>(Constants.Prefab.EndGameMenu));
            _endGameMenu.SetActive(false);
        }

        private new void Start()
        {
            base.Start();
            InitializePlayerCount();

            GameModeEvents.Instance.OnKartDestroyed.AddListener(KartDestroyed);
        }

        // PUBLIC

        public void KartDestroyed(Team team)
        {
            switch (team)
            {
                case Team.Blue:
                    _blueKartsAlive--;
                    break;
                case Team.Red:
                    _redKartsAlive--;
                    break;
                default:
                    break;
            }
            CheckIfOver();
        }

        // PROTECTED

        protected override void InitializeGame()
        {
            var karts = GameObject.FindGameObjectsWithTag(Constants.Tag.Kart);
            foreach(var kart in karts)
            {

            }
        }

        protected override void ResetGame()
        {
            if (BoltNetwork.isServer)
            {
                LevelManager.Instance.ResetLevel();
            }
        }

        // PRIVATE

        private void InitializePlayerCount()
        {
            PlayerSettings[] players = FindObjectsOfType<PlayerSettings>();

            foreach(PlayerSettings player in players)
            {
                switch (player.Team)
                {
                    case Team.Blue:
                        _blueKartsAlive++;
                        break;
                    case Team.Red:
                        _redKartsAlive++;
                        break;
                    default:
                        break;
                }
            }
        }

        private void CheckIfOver()
        {
            if (_redKartsAlive <= 0)
            {
                IsOver = true;
                WinnerTeam = Team.Blue;
                EndGame();
            }
            else if (_blueKartsAlive <= 0)
            {
                IsOver = true;
                WinnerTeam = Team.Red;
                EndGame();
            }
        }

        private void EndGame()
        {
            var playerInputsList = FindObjectsOfType<MonoBehaviour>().OfType<IControllable>();

            foreach(MonoBehaviour playerInputs in playerInputsList)
            {
                playerInputs.enabled = false;
            }


            _endGameMenu.SetActive(true);
            _endGameMenu.GetComponent<Menu.GameOverMenu>().SetWinnerTeam(WinnerTeam);

            OnGameEnd.Invoke(WinnerTeam);
        }
    }
}
