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

        [Header("UI Elements")]
        [SerializeField] private GameObject _battleMapsPool;
        [SerializeField] private GameObject _orbMapsPool;

        // CORE

        private void Start()
        {
            InitializeMapPoolAndListener();
        }

        private void OnEnable()
        {
            InitializeMapPoolAndListener();
        }

        // PUBLIC

        public void InitializeBattleMapPool()
        {
            _battleMapsPool.SetActive(true);
            InitializeMapPoolAndListener();
        }

        public void InitializeOrbMapPool()
        {
            _orbMapsPool.SetActive(true);
            InitializeMapPoolAndListener();
        }

        public void ResetMapPool()
        {
            _lobbyData.MapPool.Clear();
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
                Debug.Log("adding : " + mapName);
                _lobbyData.AddMap(gameModeName, mapName);
            }
            else
            {
                Debug.Log("rm : " + mapName);
                _lobbyData.RemoveMap(gameModeName, mapName);
            }
        }
    }
}
