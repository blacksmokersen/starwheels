using UnityEngine;
using Multiplayer.Teams;

namespace GameModes.Totem
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server, BoltScenes.CarapaceDebug, BoltScenes.CarapaceOvale, BoltScenes.CarapaceSquare)]
    public class TotemServer : GameModeBase
    {
        // BOLT

        public override void OnEvent(TotemWallHit evnt)
        {
            Debug.Log("TotemWallHit event received.");
            var team = TeamsColors.GetTeamFromColor(evnt.Team);
            IncreaseScore(team);
        }
    }
}
