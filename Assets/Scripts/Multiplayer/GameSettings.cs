using UnityEngine;
using Multiplayer.Teams;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public string Gamemode;
    public GamemodesTeamsListSettings GamemodesTeamsListSettings;
    public TeamsListSettings TeamsListSettings;
    public string MapName;
    public string MaxPlayerCount;
    public string CurrentPlayerCount;
    public bool Respawn;

    public void SetGamemode(string gamemode)
    {
        Gamemode = gamemode;
        TeamsListSettings = GamemodesTeamsListSettings.FindTeamsListWithGamemode(gamemode);
    }
}
