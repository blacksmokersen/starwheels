using UnityEngine;

namespace Abilities
{
    public class AbilitySettings : ScriptableObject
    {
        [Header("Cooldown")]
        public float CooldownDuration;

        [Header("Reload")]
        public GameObject ReloadParticlePrefab;
        public int ReloadParticleNumber;
    }
}
