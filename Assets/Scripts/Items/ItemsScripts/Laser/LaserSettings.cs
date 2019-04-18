using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items Settings/Laser")]
    public class LaserSettings : ScriptableObject
    {
        [Header("Wall Duration")]
        public float LaserDuration;
        [Header("KartRecoil")]
        public float KartRecoilPower;
    }
}
