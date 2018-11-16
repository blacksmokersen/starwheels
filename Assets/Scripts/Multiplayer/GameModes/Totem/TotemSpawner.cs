using Bolt;

namespace GameModes.Totem
{
    public class TotemSpawner : GlobalEventListener
    {
        private bool _totemInstantiated = false;

        public override void BoltStartDone()
        {
            InstantiateTotem();
        }

        public override void SceneLoadLocalDone(string map)
        {
            InstantiateTotem();
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
