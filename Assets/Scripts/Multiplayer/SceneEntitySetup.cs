using Bolt;

namespace Multiplayer
{
    public class SceneEntitySetup : GlobalEventListener
    {
        // BOLT

        public override void BoltStartDone()
        {
            if (BoltNetwork.isServer)
            {
                SetupEntity();
            }
        }

        public override void SceneLoadLocalDone(string map)
        {
            if (BoltNetwork.isServer)
            {
                SetupEntity();
            }
        }

        // PRIVATE

        private void SetupEntity()
        {
            var entity = GetComponent<BoltEntity>();
            BoltNetwork.Attach(entity.gameObject);
            entity.TakeControl();
        }
    }
}
