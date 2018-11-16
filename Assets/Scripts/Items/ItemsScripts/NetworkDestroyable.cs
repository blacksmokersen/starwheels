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
                Debug.Log("What");
                if (timeBeforeDestroy != 0)
                    BoltEntity.Destroy(gameObject, timeBeforeDestroy);
                else
                {
                    Debug.Log("is");
                    entity.TakeControl();
                    Debug.Log("up");
                    BoltNetwork.Destroy(gameObject);
                    Debug.Log("man");
                }
            }
            else
            {
                MonoBehaviour.Destroy(gameObject, timeBeforeDestroy);
            }
        }
    }
}
