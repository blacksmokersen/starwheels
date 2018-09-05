using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace MyExtensions
{
    public class Functions
    {
        public static float RemapValue(float actualMin, float actualMax, float targetMin, float targetMax, float val)
        {
            return targetMin + (targetMax - targetMin) * ((val - actualMin) / (actualMax - actualMin));
        }

        public static List<Player> GetTeammates()
        {
            var teammates = new List<Player>();
            foreach (Player player in PhotonNetwork.PlayerListOthers)
            {
                if(player.GetTeam() == PhotonNetwork.LocalPlayer.GetTeam())
                {
                    teammates.Add(player);
                }
            }
            return teammates;
        }
    }
}
