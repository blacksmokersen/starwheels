using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class GamemodePoolUpdater : MonoBehaviour
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        // CORE

        private void Awake()
        {
            InitializeGamemodePoolAndListeners();
        }

        // PRIVATE

        private void InitializeGamemodePoolAndListeners()
        {
            _lobbyData.GamemodePool = new List<string>();
            _lobbyData.MapPool = new Dictionary<string, List<string>>();

            var gamemodesToggles = GetComponentsInChildren<Toggle>();
            foreach (var toggle in gamemodesToggles)
            {
                var gameModeName = toggle.GetComponentInChildren<TextMeshProUGUI>().text;
                if (toggle.isOn)
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
                _lobbyData.GamemodePool.Add(gameMode);
            }
            else
            {
                _lobbyData.GamemodePool.Remove(gameMode);
            }
        }
    }
}
