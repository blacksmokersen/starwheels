using System.Collections;
using UnityEngine;
using Bolt;

namespace Items
{
    public class NetworkDestroyable : EntityBehaviour
    {
        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (BoltNetwork.IsConnected && entity.isAttached && BoltNetwork.IsServer)
            {
                if (timeBeforeDestroy != 0)
                {
                    StartCoroutine(DestroyAfterXSeconds(timeBeforeDestroy));
                }
                else
                {
                    DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                    destroyEntityEvent.Entity = entity;
                    destroyEntityEvent.Send();
                }
            }
            else if(!BoltNetwork.IsConnected)
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
