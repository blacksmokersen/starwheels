using UnityEngine;
using Multiplayer;
using Multiplayer.Teams;

namespace Gamemodes
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server, BoltScenes.Pillars, BoltScenes.BorealCave, BoltScenes.RubberDistrict)]
    public class ClassicBattle : GameModeBase
    {
        [Header("Battle Settings")]
        public int MaxPlayersPerTeam;

        private int _maxScore = 15;
        private int _redKartsAlive;
        private int _blueKartsAlive;

        // CORE

        private void Awake()
        {
            CurrentGameMode = GameMode.ClassicBattle;
        }

        // BOLT

        public override void SceneLoadLocalDone(string map)
        {
            _redKartsAlive = 0;
            _blueKartsAlive = 0;
        }

        public override void OnEvent(PlayerHit evnt)
        {
            var team = evnt.KillerTeamColor.GetTeam();
            IncreaseScore(team);
            CheckScore();
        }

        public override void OnEvent(PlayerReady evnt)
        {
            var team = evnt.Team.GetTeam();
            PlayerJoined(team);
        }

        // PUBLIC

        public void PlayerJoined(Team team)
        {
            switch (team)
            {
                case Team.Blue:
                    _blueKartsAlive++;
                    break;
                case Team.Red:
                    _redKartsAlive++;
                    break;
            }
        }

        public void KartDestroy(Team team)
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
            CheckIfAnyKartsAlive();
        }

        // PROTECTED

        protected override void InitializeGame()
        {
            var karts = GameObject.FindGameObjectsWithTag(Constants.Tag.Kart);
            foreach(var kart in karts)
            {
                // Do stuff ?
            }
        }

        // PRIVATE

        private void InitializePlayerCount()
        {
            Player[] players = FindObjectsOfType<Player>();

            foreach(Player player in players)
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

        private void CheckIfAnyKartsAlive()
        {
            if (_redKartsAlive <= 0 && _blueKartsAlive <= 0)
            {
                IsOver = true;
                WinnerTeam = Team.None;
                EndGame();
            }
            else if (_redKartsAlive <= 0)
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

        private void CheckScore()
        {
            if(_blueScore >= _maxScore)
            {
                IsOver = true;
                WinnerTeam = Team.Blue;
                EndGame();
            }
            else if(_redScore >= _maxScore)
            {
                IsOver = true;
                WinnerTeam = Team.Red;
                EndGame();
            }
        }
    }
}
