using UnityEngine;
using Bolt;

namespace Network
{
    public class KartNetworkBehaviour : EntityBehaviour<IKartState>
    {
        private void Awake()
        {
            if (!BoltNetwork.isConnected)
            {
                BoltLauncher.StartServer();
            }
        }

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
            state.SetAnimator(GetComponentInChildren<Animator>());
            state.Animator.applyRootMotion = entity.isOwner;
        }
    }
}
