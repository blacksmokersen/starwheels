using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public Texture2D InventoryTexture;
    public ItemBehaviour ItemPrefab;
}
