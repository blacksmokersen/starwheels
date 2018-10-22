using UnityEngine;

namespace Items
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        public int Count;
        public string ItemName;
        public Sprite InventoryTexture;
        public NetworkDestroyable ItemPrefab;
        public float Chances;
        public Color ItemColor;
    }
}
