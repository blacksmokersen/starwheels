using UnityEngine;
using Bolt;

namespace Items
{
    public class NetworkDestroyable : EntityBehaviour
    {
        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (BoltNetwork.isConnected)
            {
                if (timeBeforeDestroy != 0f && entity.isOwner)
                    BoltEntity.Destroy(gameObject, timeBeforeDestroy);
                else
                    BoltNetwork.Destroy(gameObject);
            }
            else
            {
                MonoBehaviour.Destroy(gameObject, timeBeforeDestroy);
            }
        }

        private void MultiplayerDestroy()
        {
            BoltNetwork.Destroy(gameObject);
        }
    }
}
