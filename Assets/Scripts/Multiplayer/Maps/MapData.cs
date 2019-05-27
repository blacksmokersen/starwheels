using UnityEngine;

[CreateAssetMenu(menuName = "Map Data")]
public class MapData : ScriptableObject
{
    public string MapName;
    public int MaxPlayers;
    public StringVariable ExclusiveGameMode;
}
