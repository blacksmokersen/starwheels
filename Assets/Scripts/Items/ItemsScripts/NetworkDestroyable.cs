using System.Collections;
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
                if (timeBeforeDestroy != 0)
                {
                    StartCoroutine(DestroyAfterXSeconds(10f));
                }
                else
                {
                    entity.TakeControl();
                    BoltNetwork.Destroy(gameObject);
                }
            }
            else if(!BoltNetwork.isConnected)
            {
                MonoBehaviour.Destroy(gameObject, timeBeforeDestroy);
            }
        }

        private IEnumerator DestroyAfterXSeconds(float x)
        {
            yield return new WaitForSeconds(x);
            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
            destroyEntityEvent.Entity = entity;
            destroyEntityEvent.Send();
        }
    }
}
