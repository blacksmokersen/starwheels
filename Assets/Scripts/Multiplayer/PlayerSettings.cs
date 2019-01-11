﻿using UnityEngine;

namespace Multiplayer
{
    [CreateAssetMenu(menuName = "Multiplayer Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Player Information")]
        public Color TeamColor;
        public string Nickname;
        public int ConnectionID;
        public int KartIndex;
        public int AbilityIndex;
    }
}
