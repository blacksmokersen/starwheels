using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Multiplayer.Teams;

namespace Gamemodes
{
    public enum GameMode { None, ClassicBattle, BankRobbery, GoldenTotem }

    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public abstract class GameModeBase : GlobalEventListener
    {
        public static GameMode CurrentGameMode;

        [Header("Game Mode")]
        public Team WinnerTeam = Team.None;
        public bool GameStarted = false;
        public bool IsOver = false;

        [Header("Events")]
        public UnityEvent OnGameReset;
        public TeamEvent OnGameEnd;
        public UnityEvent OnGameStart;

        protected int _redScore = 0;
        protected int _blueScore = 0;

        [SerializeField] private float countdownSeconds = 3f;

        // BOLT

        public override void SceneLoadLocalDone(string scene)
        {
            ResetGame();
        }

        public override void SceneLoadLocalDone(string scene, IProtocolToken token)
        {
            ResetGame();
        }

        // PROTECTED

        protected virtual void InitializeGame()
        {
            // To Implement in concrete Game Modes
            OnGameStart.Invoke();
        }

        protected void EndGame()
        {
            GameOver goEvent = GameOver.Create();
            goEvent.WinningTeam = WinnerTeam.GetColor();
            goEvent.Send();

            var playerInputsList = FindObjectsOfType<MonoBehaviour>().OfType<IControllable>();

            foreach (MonoBehaviour playerInputs in playerInputsList)
            {
                playerInputs.enabled = false;
            }
        }

        protected void ResetGame()
        {
            WinnerTeam = Team.None;
            GameStarted = false;
            IsOver = false;
            _redScore = 0;
            _blueScore = 0;
            OnGameReset.Invoke();
        }

        protected void IncreaseScore(Team team)
        {
            ScoreIncreased scoreIncreased = ScoreIncreased.Create();
            scoreIncreased.Team = team.GetColor();
            switch (team)
            {
                case Team.Blue:
                    _blueScore++;
                    scoreIncreased.Score = _blueScore;
                    break;
                case Team.Red:
                    _redScore++;
                    scoreIncreased.Score = _redScore;
                    break;
                default:
                    Debug.LogWarning("Unknown team.");
                    break;
            }
            scoreIncreased.Send();
        }

        // PRIVATE

        private IEnumerator StartCountdown()
        {
            yield return new WaitForSeconds(countdownSeconds);
            InitializeGame();
            GameStarted = true;
        }
    }
}
