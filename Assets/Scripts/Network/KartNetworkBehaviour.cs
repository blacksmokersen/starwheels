using UnityEngine;
using Bolt;

namespace Network
{
    public class KartNetworkBehaviour : EntityBehaviour<IKartState>
    {
        private void Awake()
        {
            Debug.Log("Bolt is connected : " + BoltNetwork.isConnected);
            if (!BoltNetwork.isConnected)
            {
                BoltLauncher.StartSinglePlayer();
            }
        }

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
        }
    }
}
