using System.Collections;
using UnityEngine;

namespace Items
{
    /*
     * Base item class for handling the instantiation and destroy
     * 
     */ 
    [RequireComponent(typeof(PhotonView))]
    public class ItemBehaviour : MonoBehaviour
    {
        public virtual void Spawn(KartInventory kart, Directions direction)
        { }

        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (PhotonNetwork.connected)
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
            var view = GetComponent<PhotonView>();
            if (view.isMine)
            {
                PhotonNetwork.RemoveRPCs(view);
                //PhotonNetwork.RemoveRPCsInGroup(view.group);
                PhotonNetwork.Destroy(view);
            }
        }
    }
}
