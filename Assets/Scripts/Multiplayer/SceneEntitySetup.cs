using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class SceneEntitySetup : GlobalEventListener
    {
        // BOLT

        public override void BoltStartDone()
        {
            SetupEntity();
        }

        public override void SceneLoadLocalDone(string map)
        {
            SetupEntity();
        }

        // PRIVATE

        private new void OnEnable()
        {
            SetupEntity();
        }

        private void SetupEntity()
        {
            var entity = GetComponent<BoltEntity>();
            if (BoltNetwork.IsServer && !entity.isAttached)
            {
                BoltNetwork.Attach(entity.gameObject);
                entity.TakeControl();
            }
        }
    }
}
