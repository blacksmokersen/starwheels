using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities Settings/Wall")]
    public class WallSettings : AbilitySettings
    {
        [Header("Wall Duration")]
        public float WallDuration;
         [Header("Wall Range")]
        public float WallBackPosition;
        public float WallFrontPosition;
    }
}
