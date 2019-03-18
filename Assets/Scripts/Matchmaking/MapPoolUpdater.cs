using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class MapPoolUpdater : MonoBehaviour
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        // CORE

        private void Start()
        {
            InitializeMapPoolAndListener();
        }

        private void OnEnable()
        {
            InitializeMapPoolAndListener();
        }

        // PRIVATE

        private void InitializeMapPoolAndListener()
        {
            var toggles = GetComponentsInChildren<Toggle>();

            foreach (var toggle in toggles)
            {
                var mapName = toggle.GetComponent<MapLabel>().MapData.MapName;
                var gameModeNames = toggle.GetComponentsInParent<GamemodeGroupLabel>();
                foreach (var gamemodeName in gameModeNames)
                {
                    toggle.onValueChanged.AddListener((b) =>
                    {

                        UpdateMapPool(mapName, gamemodeName.Label.Value, toggle.isOn);

                    });

                    if (toggle.isOn)
                    {
                        _lobbyData.MapPool[gamemodeName.Label.Value].Add(mapName);
                    }
                }
            }
        }

        private void UpdateMapPool(string mapName, string gameModeName, bool toggleIsOn)
        {
            if (toggleIsOn)
            {
                _lobbyData.AddMap(gameModeName, mapName);
            }
            else
            {
                _lobbyData.RemoveMap(gameModeName, mapName);
            }
        }
    }
}
