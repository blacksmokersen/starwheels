using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Lottery
{
    [Serializable]
    public struct ItemsChances
    {
        public Item Item;
        public float Chances;
    }

    [CreateAssetMenu(menuName ="Item Settings/Item Box")]
    public class ItemBoxSettings : ScriptableObject
    {
        [Header("Colors")]
        public Color SphereColor;
        public Color SphereCenterColor;
        public Color SphereSurroundingParticlesColor;
        public Color CenterLightColor;

        [Header("Items Chances")]
        public List<ItemsChances> ItemsChances;

        [Header("Upgrade")]
        public float SecondsBeforeUpgrade;
        public ItemBoxSettings NextUpgradeSettings;
    }
}
