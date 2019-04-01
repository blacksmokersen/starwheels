using UnityEngine;

namespace Gamemodes.Totem
{
    public class TotemServerRules : GamemodeBase
    {
        // BOLT

        public override void OnEvent(TotemWallHit evnt)
        {
            if (BoltNetwork.IsServer)
            {
                Debug.Log("TotemWallHit event received.");
                IncreaseScore(evnt.Team.ToTeam().OppositeTeam(), 1);
                SendScoreIncreasedEvent(evnt.Team.ToTeam().OppositeTeam());
            }
        }
    }
}
