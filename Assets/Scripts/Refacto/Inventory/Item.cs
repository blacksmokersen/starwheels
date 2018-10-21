using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public Effect Effect;
    public int Count;
    public Texture2D Icon;
}
