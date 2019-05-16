using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer.Teams
{
    public class TeamsColors
    {
        public static Color NoTeamColor = Color.white;
        public static Color BlueColor = new Color(0.2f, 0.5f, 1f, 0.8f);
        public static Color RedColor = new Color(1f, 0.5f, 0.2f, 0.8f);

        public static Dictionary<Team, Color> TeamToColor = new Dictionary<Team, Color>
        {
            { Team.None, NoTeamColor},
            { Team.Blue, BlueColor},
            { Team.Red,  RedColor}
        };

        public static void SetMaterialColorFromTeam(Material material, Team team)
        {
            material.color = TeamToColor[team];
        }

        public static Team GetTeamFromColor(Color color)
        {
            foreach (KeyValuePair<Team, Color> entry in TeamToColor)
            {
                if (entry.Value == color)
                    return entry.Key;
            }
            return Team.None;
        }
    }
}
