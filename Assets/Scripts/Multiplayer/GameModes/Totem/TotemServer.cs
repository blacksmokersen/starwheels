using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameModes
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server)]
    public class TotemServer : GameModeBase
    {
        public override void OnEvent(WallHit evnt)
        {

        }
    }
}
