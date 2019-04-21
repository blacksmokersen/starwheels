using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items Settings/Laser")]
    public class LaserSettings : ScriptableObject
    {
        [Header("Laser Duration")]
        public float FlashDuration;
        public float TimeBeforeDestroyLaser;
        [Header("KartRecoil")]
        public float KartRecoilPower;
    }
}
