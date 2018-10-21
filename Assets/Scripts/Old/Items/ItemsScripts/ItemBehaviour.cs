using System.Collections;
using UnityEngine;

namespace Items
{
    /*
     * Base item class for handling the instantiation and destroy
     *
     */
    public class ItemBehaviour : MonoBehaviour
    {
        public ItemData ItemData;
        public virtual void Spawn(KartInventory kart, Direction direction,float aimAxis)
        { }

        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (BoltNetwork.isConnected)
            {
                StartCoroutine(DelayedDestroy(timeBeforeDestroy));
            }
            else
            {
                UnityEngine.MonoBehaviour.Destroy(gameObject, timeBeforeDestroy);
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
