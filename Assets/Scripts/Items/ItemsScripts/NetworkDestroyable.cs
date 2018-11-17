using UnityEngine;
using Bolt;

namespace Items
{
    public class NetworkDestroyable : EntityBehaviour
    {
        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (BoltNetwork.isConnected && entity.isAttached && BoltNetwork.isServer)
            {
                entity.TakeControl();
                BoltNetwork.Destroy(gameObject);
            }
            else if(!BoltNetwork.isConnected)
            {
                MonoBehaviour.Destroy(gameObject, timeBeforeDestroy);
            }
        }
    }
}
