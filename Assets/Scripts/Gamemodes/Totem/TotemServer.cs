using UnityEngine;

namespace Gamemodes.Totem
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server, BoltScenes.CarapaceDebug, BoltScenes.StarwheelsFactory,BoltScenes.CarapaceSquare_V4)]
    public class TotemServer : GameModeBase
    {
        // BOLT

        public override void OnEvent(TotemWallHit evnt)
        {
            Debug.Log("TotemWallHit event received.");
            IncreaseScore(evnt.Team.ToTeam().OppositeTeam());
        }
    }
}
