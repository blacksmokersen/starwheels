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
                StartCoroutine(DelayedDestroy(timeBeforeDestroy));
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
