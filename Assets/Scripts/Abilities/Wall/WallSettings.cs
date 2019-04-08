using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities Settings/Wall")]
    public class WallSettings : AbilitySettings
    {
        [Header("Wall Duration")]
        public float WallDuration;

    }
}
