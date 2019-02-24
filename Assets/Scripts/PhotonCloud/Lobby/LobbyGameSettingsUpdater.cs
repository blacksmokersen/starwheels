using UnityEngine;
using UnityEngine.Events;
using Bolt;
using TMPro;
using MyExtensions;
using Multiplayer.Teams;

namespace Photon.Lobby
{
    public class LobbyGameSettingsUpdater : EntityBehaviour<ILobbyGameState>
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Dropdown _gameModeDropdown;
        [SerializeField] private TMP_Dropdown _mapDropdown;

        [Header("Teams for Gamemodes")]
        [SerializeField] private TeamsListSettings _totemTeamList;
        [SerializeField] private TeamsListSettings _FFATeamList;
        [SerializeField] private TeamsListSettings _battleTeamList;

        [Header("Unity Events")]
        public UnityEvent OnGameModeUpdated;
        public UnityEvent OnMapUpdated;
        public UnityEvent OnMaxPlayerCountUpdated;
        public UnityEvent OnPlayerCountUpdated;
        public UnityEvent OnPublicGameUpdated;

        private GameSettings _gameSettings;

        // CORE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>("GameSettings");
            UpdateColorsAccordingToGameMode(_gameModeDropdown.options[_gameModeDropdown.value].text);

            if (BoltNetwork.IsServer)
            {
                _gameModeDropdown.onValueChanged.AddListener((i) =>
                {
                    state.GameMode = _gameModeDropdown.options[i].text;
                });

                _mapDropdown.onValueChanged.AddListener((i) =>
                {
                    state.Map = _mapDropdown.options[i].text;
                });
            }
        }

        // BOLT

        public override void Attached()
        {
            state.AddCallback("GameMode", UpdateGameMode);
            state.AddCallback("MapName", UpdateMap);
            state.AddCallback("MaxPlayerCount", UpdateMaxPlayerCount);
            state.AddCallback("PublicGame", UpdatePublicGame);

            if (entity.isOwner)
            {
                DontDestroyOnLoad(gameObject);
                state.GameMode = _gameModeDropdown.options[_gameModeDropdown.value].text;
                //state.Map = _mapDropdown.options[_mapDropdown.value].text;
            }
        }

        // PRIVATE

        private void UpdateGameMode()
        {
            Debug.Log("Updating game mode");
            _gameModeDropdown.ChangeTMProDropdownValue(state.GameMode);
            _gameSettings.GameMode = state.GameMode;
            UpdateColorsAccordingToGameMode(state.GameMode);

            if(OnGameModeUpdated != null) OnGameModeUpdated.Invoke();
        }

        private void UpdateColorsAccordingToGameMode(string gameMode)
        {
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

        private void UpdateMap()
        {
            _mapDropdown.ChangeTMProDropdownValue(state.Map);
            _gameSettings.MapName = state.Map;

            if (OnMapUpdated != null) OnMapUpdated.Invoke();
        }

        private void UpdateMaxPlayerCount()
        {
            if (OnMaxPlayerCountUpdated != null) OnMaxPlayerCountUpdated.Invoke();
        }

        private void UpdateCurrentPlayerCount()
        {
            if (OnPlayerCountUpdated != null) OnPlayerCountUpdated.Invoke();
        }

        private void UpdatePublicGame()
        {
            if (OnPublicGameUpdated != null) OnPublicGameUpdated.Invoke();
        }
    }
}
