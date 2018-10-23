using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu]
    public class AbilitySettings : ScriptableObject
    {
        public float Cooldown;
        public GameObject Prefab;
        public GameObject ReloadParticlePrefab;
        public int ReloadParticleNumber;
    }
}
