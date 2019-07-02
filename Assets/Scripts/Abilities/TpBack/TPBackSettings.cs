using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities Settings/TPBack")]
    public class TPBackSettings : AbilitySettings
    {
        [Header("TPBack")]
        public GameObject Prefab;
        [Header("Settings")]
        public float IncreasedYPositionOnTP;
    }
}
