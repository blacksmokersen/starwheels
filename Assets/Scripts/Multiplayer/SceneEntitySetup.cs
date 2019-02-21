using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class SceneEntitySetup : GlobalEventListener
    {
        // BOLT

        public override void BoltStartDone()
        {
            Debug.Log("BoltStart");
            SetupEntity();
        }

        public override void SceneLoadLocalDone(string map)
        {
            Debug.Log("Sceneloaded");
            SetupEntity();
        }

        // PRIVATE

        private new void OnEnable()
        {
            Debug.Log("OnEnable");
            SetupEntity();
        }

        private void SetupEntity()
        {
            if (BoltNetwork.IsServer)
            {
                var entity = GetComponent<BoltEntity>();
                BoltNetwork.Attach(entity.gameObject);
                entity.TakeControl();
            }
            /*
            else
            {
                gameObject.SetActive(false);
            }
            */
        }
    }
}
