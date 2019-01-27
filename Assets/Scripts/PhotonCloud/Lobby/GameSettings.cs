using UnityEngine;
using Multiplayer.Teams;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public string GameMode;
    public TeamsListSettings TeamsListSettings;
    public string MapName;
    public string MaxPlayerCount;
    public string CurrentPlayerCount;
    public bool Respawn;
}
