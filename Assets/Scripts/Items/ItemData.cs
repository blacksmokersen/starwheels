using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int Count;
    public string ItemName;
    public Texture2D InventoryTexture;
    public ItemBehaviour ItemPrefab;
}
