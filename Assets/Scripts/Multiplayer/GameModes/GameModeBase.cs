using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace GameModes
{
    public enum GameMode { None, ClassicBattle, BankRobbery, GoldenTotem }

    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class GameModeBase : GlobalEventListener
    {
        public static GameMode CurrentGameMode;

        public bool GameStarted = false;

        [Header("Events")]
        public UnityEvent OnGameReset;
        public TeamEvent OnGameEnd;
        public UnityEvent OnGameStart;

        [SerializeField] private float countdownSeconds = 3f;

        // CORE

        // PUBLIC

        // PROTECTED

        protected virtual void InitializeGame()
        {
            // To Implement in concrete Game Modes
            OnGameStart.Invoke();
        }

        protected virtual void ResetGame()
        {
            // To Implement in concrete Game Modes
            OnGameReset.Invoke();
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
