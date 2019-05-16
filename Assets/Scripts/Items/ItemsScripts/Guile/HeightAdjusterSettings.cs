using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Item/Height Adjustment")]
    public class HeightAdjusterSettings : ScriptableObject
    {
        public bool AdjustingOnStart;
        public float AdjustmentSpeed;
        public List<float> LevelsHeights;
    }
}
