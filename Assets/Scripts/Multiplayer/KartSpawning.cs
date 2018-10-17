using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class KartSpawning : EntityBehaviour
    {
        // BOLT

        public override void ControlGained()
        {
            if(BoltNetwork.isClient)
                FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(gameObject);
        }
    }
}
