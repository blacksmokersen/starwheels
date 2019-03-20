using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer.Teams
{
    [CreateAssetMenu(menuName = "Team Settings/GamemodesTeamsListSettings")]
    public class GamemodesTeamsListSettings : ScriptableObject
    {
        public List<TeamsListSettings> TeamsLists;

        public TeamsListSettings FindTeamsListWithGamemode(string gamemode)
        {
            foreach (var list in TeamsLists)
            {
                if (list.SpecificGameMode == gamemode)
                {
                    return list;
                }
            }
            return null;
        }
    }
}
