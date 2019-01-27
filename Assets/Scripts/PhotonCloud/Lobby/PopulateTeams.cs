using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Multiplayer.Teams;

namespace Photon.Lobby
{
    public class PopulateTeams : MonoBehaviour
    {
        [Header("Teams for Gamemodes")]
        [SerializeField] private TeamsListSettings _totemTeamList;
        [SerializeField] private TeamsListSettings _FFATeamList;
        [SerializeField] private TeamsListSettings _battleTeamList;

        private GameSettings _gameSettings;

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>("GameSettings");
        }

        public void SetColorsAccordingToGameMode(string gameMode)
        {
            _gameSettings.GameMode = gameMode;
            switch (gameMode)
            {
                case Constants.GameModes.Battle:
                    _gameSettings.TeamsListSettings = _battleTeamList;
                    break;
                case Constants.GameModes.Totem:
                    _gameSettings.TeamsListSettings = _battleTeamList;
                    break;
                case Constants.GameModes.FFA:
                    _gameSettings.TeamsListSettings = _FFATeamList;
                    break;
            }
        }

        public void SetMap(string mapName)
        {
            _gameSettings.MapName = mapName;
        }
    }
}
