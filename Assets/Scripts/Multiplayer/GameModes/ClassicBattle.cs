using UnityEngine;

namespace GameModes
{
    public class ClassicBattle : GameModeBase
    {
        public static ClassicBattle Instance;

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
            GameModeEvents.Instance.OnGameReset.AddListener(ResetGame);
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
                //kart.GetComponent<>
            }
        }

        protected override void ResetGame()
        {
            /*if (PhotonNetwork.IsMasterClient)
            {
                LevelManager.Instance.ResetLevel();
            }
            */
        }

        // PRIVATE

        private void InitializePlayerCount()
        {
            /*Player[] players = PhotonNetwork.PlayerList;

            foreach(Player player in players)
            {
                switch (player.GetTeam())
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
            */
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
            /*var playerInputsList = FindObjectsOfType<PlayerInputs>();

            foreach(PlayerInputs playerInputs in playerInputsList)
            {
                playerInputs.Enabled = false;
            }


            _endGameMenu.SetActive(true);
            _endGameMenu.GetComponent<Menu.GameOverMenu>().SetWinnerTeam(WinnerTeam);

            GameModeEvents.Instance.OnGameEnd(WinnerTeam);*/
        }
    }
}
