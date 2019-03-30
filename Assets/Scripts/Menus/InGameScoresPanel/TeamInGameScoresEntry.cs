using UnityEngine;
using UnityEngine.UI;
using Multiplayer.Teams;

namespace Menu.InGameScores
{
    [DisallowMultipleComponent]
    public class TeamInGameScoresEntry : MonoBehaviour
    {
        public Team Team;

        [Header("UI Elements")]
        [SerializeField] private Image _backgroundImage;

        private GameSettings _gameSettings;

        // CORE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
        }

        // PUBLIC

        public void SetTeam(Team team)
        {
            Team = team;
        }

        public void SetColorAccordingToTeam()
        {
            _backgroundImage.color = _gameSettings.TeamsListSettings.GetSettings(Team).KillFeedEntryColor;
        }
    }
}
