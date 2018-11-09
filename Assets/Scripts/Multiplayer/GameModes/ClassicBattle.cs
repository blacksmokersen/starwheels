using UnityEngine;
using Multiplayer;
using Multiplayer.Teams;

namespace GameModes
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server, BoltScenes.FortBlock, BoltScenes.Pillars)]
    public class ClassicBattle : GameModeBase
    {
        [Header("Battle Settings")]
        public int MaxPlayersPerTeam;

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

        public override void OnEvent(KartDestroyed evnt)
        {
            //KartDestroy(TeamsColors.GetTeamFromColor(evnt.Team));
        }

        public override void OnEvent(PlayerReady evnt)
        {
            var team = TeamsColors.GetTeamFromColor(evnt.Team);
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
            CheckIfOver();
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

        private void CheckIfOver()
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
    }
}
