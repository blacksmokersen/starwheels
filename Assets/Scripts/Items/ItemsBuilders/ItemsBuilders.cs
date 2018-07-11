using UnityEngine;

namespace Items {

    [RequireComponent(typeof(DiskBuilder))]
    [RequireComponent(typeof(RocketBuilder))]
    [RequireComponent(typeof(NitroBuilder))]
    public class ItemsBuilders : MonoBehaviour {

        public static ItemsBuilders Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public IItem GetBuilder(ItemTypes itemType)
        {
            switch (itemType)
            {
                case ItemTypes.Disk:
                    return GetComponent<DiskBuilder>();
                case ItemTypes.Rocket:
                    return GetComponent<RocketBuilder>();
                case ItemTypes.Nitro:
                    return GetComponent<NitroBuilder>();
                default:
                    return null;
            }
        }
    }
}