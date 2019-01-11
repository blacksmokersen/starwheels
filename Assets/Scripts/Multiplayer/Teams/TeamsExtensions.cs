using System.Collections.Generic;
using UnityEngine;
using Multiplayer.Teams;

public static class TeamsExtensions
{
    public static Team OppositeTeam(this Team team)
    {
        switch (team)
        {
            case Team.Blue:
                return Team.Red;
            case Team.Red:
                return Team.Blue;
            default:
                return Team.None;
        }
    }

    public static Team GetTeam(this Color color)
    {
        return TeamsColors.GetTeamFromColor(color);
    }

    public static Color GetColor(this Team team)
    {
        return TeamsColors.TeamToColor[team];
    }

    public static Color GetNextTeamColor(this Color teamColor)
    {
        var colors = new List<Color>(TeamsColors.TeamToColor.Values);

        for (int i = 0; i < colors.Count; i++)
        {
            if(teamColor == colors[i])
            {
                return colors[(i + 1) % colors.Count];
            }
        }

        return Color.white;
    }
}
