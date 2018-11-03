using System.Collections;
using UnityEngine;

namespace Items
{
    public class NetworkDestroyable : MonoBehaviour
    {
        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (BoltNetwork.isConnected)
            {
                if (timeBeforeDestroy != 0f)
                    BoltEntity.Destroy(gameObject, timeBeforeDestroy);
                else
                    BoltNetwork.Destroy(gameObject);
            }
            else
            {
                MonoBehaviour.Destroy(gameObject, timeBeforeDestroy);
            }
        }

        private IEnumerator DelayedDestroy(float t)
        {
            yield return new WaitForSeconds(t);
            MultiplayerDestroy();
        }

        private void MultiplayerDestroy()
        {
            BoltNetwork.Destroy(gameObject);
        }
    }
}
