using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class GamemodePoolUpdater : MonoBehaviour
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("Settings")]
        [SerializeField] private bool _resetTogglesOnInitializing;

        // CORE

        private void Awake()
        {
            //InitializeGamemodePoolAndListeners();
        }

        private void OnEnable()
        {
            InitializeGamemodePoolAndListeners();
        }

        // PUBLIC

        public void ResetPool()
        {
            _lobbyData.GamemodePool.Clear();
        }

        // PRIVATE

        private void InitializeGamemodePoolAndListeners()
        {
            _lobbyData.GamemodePool = new List<string>();
            _lobbyData.MapPool = new Dictionary<string, List<string>>();

            var gamemodesToggles = GetComponentsInChildren<Toggle>();

            foreach (var toggle in gamemodesToggles)
            {
                toggle.onValueChanged.RemoveAllListeners();

                var gameModeName = toggle.GetComponent<GamemodeGroupLabel>().Label.Value;

                if (_resetTogglesOnInitializing)
                {
                    toggle.isOn = false;
                }
                else if (toggle.isOn)
                {
                    _lobbyData.GamemodePool.Add(gameModeName);
                }
                _lobbyData.MapPool.Add(gameModeName, new List<string>());

                toggle.onValueChanged.AddListener((b) =>
                {
                    UpdateGamemodePool(gameModeName, toggle.isOn);
                });
            }
        }

        private void UpdateGamemodePool(string gameMode, bool toggleIsOn)
        {
            if (toggleIsOn)
            {
                _lobbyData.AddGamemode(gameMode);
            }
            else
            {
                _lobbyData.RemoveGamemode(gameMode);
            }
        }
    }
}
