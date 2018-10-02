using UnityEngine;

namespace Items
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        public int Count;
        public string ItemName;
        public Sprite InventoryTexture;
        //public ItemBehaviour ItemPrefab;
        public float Chances;
        public Color ItemColor;
    }
}
