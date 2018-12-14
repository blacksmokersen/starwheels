using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public string Name;
        public int Count;
        public Texture2D Icon;
        public ItemType ItemType;
        public GameObject itemPrefab;
        public Sprite InventoryTexture;
        public float Chances;
        public Color ItemColor;
    }
}
