using UnityEngine;

namespace Boost
{
    [CreateAssetMenu(menuName = "Tools Settings/Boost")]
    public class BoostSettings : ScriptableObject
    {
        [Header("Boost")]
        [Range(0, 10)] public float BoostDuration;
        [Range(0, 100)] public float IncreaseMaxSpeedBy;
        [Range(0, 10)] public float BoostPowerImpulse;
        [HideInInspector][Range(0, 1000)] public float BoostSpeed;
    }
}
