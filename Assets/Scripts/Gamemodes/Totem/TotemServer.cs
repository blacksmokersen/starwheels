using UnityEngine;

namespace Gamemodes.Totem
{
    public class TotemServerRules : GameModeBase
    {
        // BOLT

        public override void OnEvent(TotemWallHit evnt)
        {
            Debug.Log("TotemWallHit event received.");
            SendScoreIncreasedEvent(evnt.Team.ToTeam().OppositeTeam());
        }
    }
}
