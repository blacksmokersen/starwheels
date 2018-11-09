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

        private void SetupEntity()
        {
            if (BoltNetwork.isServer)
            {
                var entity = GetComponent<BoltEntity>();
                BoltNetwork.Attach(entity.gameObject);
                entity.TakeControl();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
