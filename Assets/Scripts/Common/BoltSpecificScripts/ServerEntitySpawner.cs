using UnityEngine;
using Bolt;

namespace Common.Bolt
{
    public class ServerEntitySpawner : GlobalEventListener
    {
        [SerializeField] private BoltEntity entity;

        // BOLT

        public override void BoltStartDone()
        {
            if (BoltNetwork.IsServer)
            {
                SpawnEntity();
            }
        }

        public override void SceneLoadLocalDone(string scene, IProtocolToken token)
        {
            if (BoltNetwork.IsServer)
            {
                SpawnEntity();
            }
        }

        // PRIVATE

        private void SpawnEntity()
        {
            BoltNetwork.Instantiate(entity, transform.position, transform.rotation);
        }
    }
}
