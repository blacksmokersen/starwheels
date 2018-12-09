using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities Settings/Cloak")]
    public class CloakSettings : AbilitySettings
    {
        [Header("Cloack")]
        public float CloakDuration;
    }
}
