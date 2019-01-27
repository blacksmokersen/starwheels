using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer.Teams
{
    [CreateAssetMenu(fileName = "TeamColorsSettings", menuName = "Team Settings/Teams List")]
    public class TeamsListSettings : ScriptableObject
    {
        [Header("Game mode")]
        public string SpecificGameMode;

        [Header("Teams")]
        public List<TeamColorSettings> TeamsList;

        public TeamColorSettings GetFirst()
        {
            return TeamsList[0];
        }

        public TeamColorSettings GetNext(TeamColorSettings current)
        {
            for (var i = 0; i < TeamsList.Count; i++)
            {
                if(TeamsList[i] == current)
                {
                    return TeamsList[(i + 1) % TeamsList.Count];
                }
            }
            return null;
        }
    }
}
