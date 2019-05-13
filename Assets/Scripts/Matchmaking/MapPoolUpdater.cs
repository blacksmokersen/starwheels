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

        // PUBLIC

        public void InitializeBattleMapPool(bool b)
        {
            if (b)
            {
                _battleMapsPool.SetActive(true);
                InitializeMapPoolAndListener();
            }
        }

        public void InitializeOrbMapPool(bool b)
        {
            if (b)
            {
                _orbMapsPool.SetActive(true);
                InitializeMapPoolAndListener();
            }
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
                toggle.onValueChanged.RemoveAllListeners();

                var mapName = toggle.GetComponent<MapLabel>().MapData.MapName;
                var gameModeNames = toggle.GetComponentsInParent<GamemodeGroupLabel>();

                foreach (var gamemodeName in gameModeNames)
                {
                    toggle.isOn = false;

                    toggle.onValueChanged.AddListener((b) =>
                    {
                        UpdateMapPool(mapName, gamemodeName.Label.Value, toggle.isOn);
                    });
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
                Debug.Log("rm : " + mapName);
                _lobbyData.RemoveMap(gameModeName, mapName);
            }
        }
    }
}
