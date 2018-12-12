using UnityEngine;

namespace GameModes.Totem
{
    [CreateAssetMenu(menuName = "Totem/Totem Settings")]
    public class TotemSettings : ScriptableObject
    {
        [Header("Slowdown Settings")]
        public float SecondsBeforeSlowdown = 1f;
        public float SlowdownFactor = 0.98f;
        public float StopMagnitudeThreshold = 0.1f;

        [Header("Speed")]
        public float Speed;

        [Header("Pick Up")]
        public float Radius;
        public float SecondsBeforeCanBePickedAgain;

        [Header("Owner")]
        public bool CanUseAbilityWhenOwner;
        public float SecondsBeforeCanUseAbilityAgain;
        public bool CanUseItemsWhenOwner;
        public float SecondsBeforeCanUseItemsAgain;
    }
}
