using UnityEngine;

namespace Items.Merge
{
    [CreateAssetMenu(menuName = "Kart Settings/Merge Settings")]
    public class ItemMergingSettings : ScriptableObject
    {
        public float FullMergeBoostSeconds;
        public float FullMergeShieldSeconds;
        public float SmallMergeBoostSeconds;
        public float SmallMergeShieldSeconds;
    }
}
