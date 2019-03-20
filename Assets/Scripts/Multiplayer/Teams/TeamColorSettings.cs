using UnityEngine;

namespace Multiplayer.Teams
{
    [CreateAssetMenu(fileName = "TeamColorsSettings", menuName ="Team Settings/Team Colors")]
    public class TeamColorSettings : ScriptableObject
    {
        [Header("Team Name")]
        public string TeamName;
        public Team TeamEnum;

        [Header("Colors")]
        public Color BoltColor;
        public Color MenuColor;
        public Color NameplateColor;
        public Color KartColor;
        public Color KillFeedEntryColor;
        public Color ItemsColor;
    }
}
