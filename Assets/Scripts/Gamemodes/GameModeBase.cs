using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Multiplayer.Teams;

namespace Gamemodes
{
    public enum GameMode { None, Battle, BankRobbery, Totem, FFA }

    public abstract class GamemodeBase : GlobalEventListener
    {
        [Header("Gamemode")]
        public Team WinnerTeam = Team.None;

        [Header("Events")]
        public UnityEvent OnGameReset;
        public TeamEvent OnGameEnd;
        public UnityEvent OnGameStart;

        protected Dictionary<Team, int> scores = new Dictionary<Team, int>();
        protected GameSettings gameSettings;

        // CORE

        private void Awake()
        {
            gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
            InitializeScores();
        }

        // BOLT

        public override void Connected(BoltConnection connection)
        {
            // Add player to score
        }

        public override void Disconnected(BoltConnection connection)
        {
            // Remove player from score
        }

        // PROTECTED

        protected virtual void InitializeGame()
        {
            OnGameStart.Invoke();
        }

        protected void SendWinningTeamEvent(Team team)
        {
            GameOver goEvent = GameOver.Create();
            goEvent.WinningTeam = (int) team;
            goEvent.Send();
        }

        protected void ResetGame()
        {
            WinnerTeam = Team.None;
            ResetScores();
            OnGameReset.Invoke();
        }

        protected void IncreaseScore(Team team, int pointsToAdd)
        {
            scores[team] += pointsToAdd;
        }

        protected void SendScoreIncreasedEvent(Team team)
        {
            ScoreIncreased scoreIncreased = ScoreIncreased.Create();
            scoreIncreased.Team = (int) team;
            scoreIncreased.Score = scores[team];
            scoreIncreased.Send();
        }

        // PRIVATE

        private void InitializeScores()
        {
            foreach (var entry in gameSettings.TeamsListSettings.TeamsList)
            {
                scores.Add(entry.TeamEnum, 0);
            }
        }

        private void ResetScores()
        {
            foreach (var entry in scores)
            {
                scores[entry.Key] = 0;
            }
        }
    }
}
