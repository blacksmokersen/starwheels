using System.Collections.Generic;
using UnityEngine;

namespace SW.Matchmaking
{
    [CreateAssetMenu(menuName = "Lobby Data")]
    public class LobbyData : ScriptableObject
    {
        [Header("General Info")]
        public string ServerName;
        public bool Public;
        public bool CanBeJoined = true;
        public int MaxPlayers;

        [Header("Gamemode Info")]
        public List<string> GamemodePool;
        public string ChosenGamemode;

        [Header("Map Info")]
        public Dictionary<string, List<string>> MapPool;
        public string ChosenMapName;

        [Header("Game Settings")]
        public GameSettings GameSettings;

        public void SetGamemode(string gamemode)
        {
            ChosenGamemode = gamemode;
            GameSettings.SetGamemode(gamemode);
        }

        public void SetRandomGamemode()
        {
            SetGamemode(PickRandomGamemodeFromPool());
        }

        public void AddGamemode(string gamemode)
        {
            if (!GamemodePool.Contains(gamemode))
            {
                GamemodePool.Add(gamemode);
            }
        }

        public void RemoveGamemode(string gamemode)
        {
            if (GamemodePool.Contains(gamemode))
            {
                GamemodePool.Remove(gamemode);
            }
        }

        public string PickRandomGamemodeFromPool()
        {
            return GamemodePool[Random.Range(0, GamemodePool.Count)];
        }

        public void SetMap(string mapName)
        {
            ChosenMapName = mapName;
            GameSettings.MapName = mapName;
        }

        public void SetRandomMap()
        {
            SetMap(PickRandomMapNameFromPool(ChosenGamemode));
        }

        public void AddMap(string gamemode, string map)
        {
            if (MapPool.ContainsKey(gamemode) && !MapPool[gamemode].Contains(map))
            {
                MapPool[gamemode].Add(map);
            }
        }

        public void RemoveMap(string gamemode, string map)
        {
            if (MapPool.ContainsKey(gamemode) && MapPool[gamemode].Contains(map))
            {
                MapPool[gamemode].Remove(map);
            }
        }

        public string PickRandomMapNameFromPool(string gamemode)
        {
            if (MapPool[gamemode].Count > 0)
            {
                return MapPool[gamemode][Random.Range(0, MapPool[gamemode].Count)];
            }
            else
            {
                Debug.LogError("MapPool is empty!");
                return null;
            }
        }

        public void SetRandomName()
        {
            ServerName = "#"+Random.Range(float.MinValue, float.MaxValue);
        }
    }
}
