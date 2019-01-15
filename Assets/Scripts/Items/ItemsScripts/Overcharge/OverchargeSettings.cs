using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Item Settings/Overcharge")]
    public class OverchargeSettings : ScriptableObject
    {
        public float OverchargeDuration = 7f;
        public float SecondsInRangeBeforeHit = 1f;
        public float OverchargeRadius;
    }
}
