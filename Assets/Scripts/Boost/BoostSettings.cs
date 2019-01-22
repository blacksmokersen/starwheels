using UnityEngine;

namespace Boost
{
    [CreateAssetMenu(menuName = "Tools Settings/Boost")]
    public class BoostSettings : ScriptableObject
    {
        [Header("Boost")]
        [Range(0, 20)] public float BoostDuration;
        [Range(0, 5)] public float BoostPercentagMultiplicator;

        [Header("ClampSpeedSettings")]
        [Range(0, 100)] public float ClampSpeedIncrease;

        [Header("ReturnToBaseClampSpeed")]
        [Range(0, 20)] public float SecondsToDecreaseClampSpeed;

        [Header("EngineBoostMode")]
        public bool IsEngineBoostActivated;
        [Range(0, 5000)] public float EngineBoostValue;

        [Header("DirectImpulse")]
        public bool HasADirectImpulse;
        [Range(0, 20000)] public float DIPower;
        [Range(0, 100)] public float DIClampSpeedIncrease;
        [Range(0, 20)] public float DIClampSpeedIncreaseDuration;
        [Range(0, 20)] public float DIClampSpeedDecreaseDuration;
    }
}
