using UnityEngine;

namespace GameModes.Totem
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server, BoltScenes.CarapaceDebug, BoltScenes.CarapaceOvale, BoltScenes.CarapaceSquare)]
    public class TotemServer : GameModeBase
    {
        // BOLT

        public override void OnEvent(TotemWallHit evnt)
        {
            Debug.Log("TotemWallHit event received.");
            var team = evnt.Team.GetTeam().OppositeTeam();
            IncreaseScore(team);
        }
    }
}
