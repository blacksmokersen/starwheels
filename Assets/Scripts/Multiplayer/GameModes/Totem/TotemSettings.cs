using UnityEngine;

namespace GameModes.Totem
{
    [CreateAssetMenu(menuName = "Totem/Totem Settings")]
    public class TotemSettings : ScriptableObject
    {
        [Header("Totem Slowdown Settings")]
        public float SecondsBeforeSlowdown = 1f;
        [Tooltip("1 means no slowdown and 0.9 quick slowdown")]
        [Range(0.9f,1f)] public float SlowdownFactor = 0.98f;
        public float StopMagnitudeThreshold = 0.1f;

        [Header("Speed")]
        public float Speed;

        [Header("Pick Up")]
        public float Radius;
        public float SecondsBeforeCanBePickedAgain;

        [Header("Owner")]
        [Tooltip("1 equals full speed and 0 still")]
        public float OwnerSpeedReductionFactor = 0.6f;
        public bool CanUseAbilityWhenOwner;
        public float SecondsBeforeCanUseAbilityAgain;
        public bool CanUseItemsWhenOwner;
        public float SecondsBeforeCanUseItemsAgain;
    }
}
