using UnityEngine;

namespace Items.Lottery
{
    [CreateAssetMenu(menuName = "Item Settings/Legendary Upgrade")]
    public class LegendaryItemBoxesSpawnerSettings : ScriptableObject
    {
        public bool UpgradeEveryXSeconds;
        public float SecondsBetweenUpgrades;
    }
}
