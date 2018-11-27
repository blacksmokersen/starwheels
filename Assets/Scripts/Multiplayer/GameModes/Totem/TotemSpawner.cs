using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    public class TotemSpawner : GlobalEventListener
    {
        private bool _totemInstantiated = false;

        // BOLT

        public override void BoltStartDone()
        {
            InstantiateTotem();
        }

        public override void SceneLoadLocalDone(string map)
        {
            InstantiateTotem();
        }

        // PUBLIC

        public void RespawnTotem()
        {
            var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
            totem.GetComponent<TotemBehaviour>().UnsetParent();
            totem.transform.position = transform.position;

            TotemThrown totemThrownEvent = TotemThrown.Create();
            totemThrownEvent.OwnerID = -1;
            totemThrownEvent.Send();
        }

        private void InstantiateTotem()
        {
            if(BoltNetwork.isConnected && BoltNetwork.isServer && !_totemInstantiated)
            {
                BoltNetwork.Instantiate(BoltPrefabs.Totem, transform.position, transform.rotation);
                _totemInstantiated = true;
            }
        }
    }
}
