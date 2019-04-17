using UnityEngine;

public enum ItemRarity { Green, Purple, Gold }

namespace Items
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public string Name;
        public ItemRarity Rarity;

        [Header("Inventory Settings")]
        public int Count;
        public ItemType ItemType;
        public GameObject ItemPrefab;
        public Sprite InventoryTexture;

        [Header("Preview")]
        public GameObject ShockwavePrefab;
        public GameObject ShieldPrefab;
        public GameObject PreviewPrefab;
        public Material EmissiveMaterial;
    }
}
