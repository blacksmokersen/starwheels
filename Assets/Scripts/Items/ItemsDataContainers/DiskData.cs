using UnityEngine;

namespace Items
{
    public class DiskData : ItemData
    {
        [Header("Disk parameters")]
        public float Speed = 500f;
        public int ReboundsBeforeBreaking = 7;
    }
}
