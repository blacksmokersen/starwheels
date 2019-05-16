using UnityEngine;
using UnityEngine.UI;
using Bolt;
using TMPro;
using Multiplayer;

namespace Gamemodes
{
    [DisallowMultipleComponent]
    public class ScoreIncreasedListener : GlobalEventListener
    {
        [Header("UI Elements")]
        [SerializeField] private Image _leftPanel;
        [SerializeField] private TextMeshProUGUI _leftScoreText;
        [SerializeField] private Image _rightPanel;
        [SerializeField] private TextMeshProUGUI _rightScoreText;

        private PlayerSettings _playerSettings;
        private GameSettings _gameSettings;

        // CORRE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
        }

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            if (_gameSettings.Gamemode == Constants.Gamemodes.FFA && evnt.FromSelf)
            {
                SetupFFAColors();
            }
        }

        public override void OnEvent(ScoreIncreased evnt)
        {
            switch (_gameSettings.Gamemode)
            {
                case Constants.Gamemodes.FFA:
                    UpdateScoreForFFA(evnt.Team.ToTeam(), evnt.Score);
                    break;
                case Constants.Gamemodes.Battle:
                    UpdateScoreForBattle(evnt.Team.ToTeam(), evnt.Score);
                    break;
                case Constants.Gamemodes.Totem:
                    UpdateScoreForTotem(evnt.Team.ToTeam(), evnt.Score);
                    break;
            }
        }

        // PRIVATE

        private void SetupFFAColors()
        {
            var myColorSettings = _playerSettings.ColorSettings;
            var otherColorSettings = _gameSettings.TeamsListSettings.GetFirst();

            if (myColorSettings == otherColorSettings)
            {
                otherColorSettings = _gameSettings.TeamsListSettings.GetNext(otherColorSettings);
            }

            _leftPanel.color = otherColorSettings.MenuColor;
            _rightPanel.color = myColorSettings.MenuColor;
        }

        private void UpdateScoreForBattle(Team team, int score)
        {
            switch (team)
            {
                case Team.Blue:
                    _leftScoreText.text = "" + score;
                    break;
                case Team.Red:
                    _rightScoreText.text = "" + score;
                    break;
                default:
                    Debug.LogWarning("Unknown team.");
                    break;
            }
        }

        private void UpdateScoreForFFA(Team team, int score)
        {
            if (team == _playerSettings.ColorSettings.TeamEnum)
            {
                _rightScoreText.text = "" + score;
            }
            else
            {
                var currentHighScore = int.Parse(_leftScoreText.text);
                if (score > currentHighScore)
                {
                    _leftPanel.color = _gameSettings.TeamsListSettings.GetSettings(team).MenuColor;
                    _leftScoreText.text = "" + score;
                }
            }
        }

        private void UpdateScoreForTotem(Team team, int score)
        {
            UpdateScoreForBattle(team, score);
        }
    }
}
