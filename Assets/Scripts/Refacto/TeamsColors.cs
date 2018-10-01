using System.Collections.Generic;
using UnityEngine;

public class TeamsColors
{
    private static Dictionary<Team, Color> _teamToColor = new Dictionary<Team, Color>
    {
        { Team.None, Color.white},
        { Team.Blue, new Color(0.2f, 0.5f, 1f, 0.8f) },
        { Team.Red,  new Color(1f, 0.5f, 0.2f, 0.8f) }
    };

    public static void SetMaterialColorFromTeam(Material material, Team team)
    {
        material.color = _teamToColor[team];
    }

    public static Color GetTeamColor(Team team)
    {
        return _teamToColor[team];
    }
}
