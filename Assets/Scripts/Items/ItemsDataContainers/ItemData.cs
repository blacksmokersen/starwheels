using UnityEngine;

namespace Items
{
    public class ItemData : ScriptableObject
    {
        [Header("Generic parameters")]
        public int Count;
        public bool Instantiable;
        public GameObject Prefab;
        public ItemTypes Type;
    }
}