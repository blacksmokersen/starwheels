﻿using System.Collections.Generic;
using UnityEngine;

namespace SW.Matchmaking
{
    [CreateAssetMenu(menuName = "Lobby Data")]
    public class LobbyData : ScriptableObject
    {
        [Header("General Info")]
        public string ServerName;
        public bool Public;
        public int MaxPlayers;

        [Header("Gamemode Info")]
        public List<string> GamemodePool;
        public string ChosenGamemode;

        [Header("Map Info")]
        public Dictionary<string, List<string>> MapPool;
        public string ChosenMapName;

        public void SetRandomGamemode()
        {
            ChosenGamemode = PickRandomGamemodeFromPool();
        }

        public string PickRandomGamemodeFromPool()
        {
            return GamemodePool[Random.Range(0, GamemodePool.Count)];
        }

        public void SetRandomMap()
        {
            ChosenMapName = PickRandomMapNameFromPool(ChosenGamemode);
        }

        public string PickRandomMapNameFromPool(string gamemode)
        {
            return MapPool[gamemode][Random.Range(0, MapPool[gamemode].Count)];
        }
    }
}
