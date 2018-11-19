using Bolt;

namespace GameModes.Totem
{
    public class TotemSpawner : GlobalEventListener
    {
        private bool _totemInstantiated = false;

        #if UNITY_EDITOR
        public override void BoltStartDone()
        {
            InstantiateTotem();
        }
        #endif

        public override void SceneLoadLocalDone(string map)
        {
            InstantiateTotem();
        }

        private void InstantiateTotem()
        {
            if(BoltNetwork.isConnected && BoltNetwork.isServer && !_totemInstantiated)
            {
                var totem = BoltNetwork.Instantiate(BoltPrefabs.Totem, transform.position, transform.rotation);                
                _totemInstantiated = true;
            }
        }
    }
}
