using UnityEngine;

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
    }
}
