using System.Collections.Generic;

namespace MyExtensions
{
    public class Functions
    {
        public static float RemapValue(float actualMin, float actualMax, float targetMin, float targetMax, float val)
        {
            return targetMin + (targetMax - targetMin) * ((val - actualMin) / (actualMax - actualMin));
        }

        public static List<PhotonPlayer> GetTeammates()
        {
            var teammates = new List<PhotonPlayer>();
            foreach (PhotonPlayer player in PhotonNetwork.otherPlayers)
            {
                if(player.GetTeam() == PhotonNetwork.player.GetTeam())
                {
                    teammates.Add(player);
                }
            }
            return teammates;
        }
    }
}
