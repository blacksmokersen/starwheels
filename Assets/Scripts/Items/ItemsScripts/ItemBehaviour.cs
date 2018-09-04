using Photon;
using System.Collections;
using UnityEngine;

namespace Items
{
    /*
     * Base item class for handling the instantiation and destroy
     *
     */
    public class ItemBehaviour : PunBehaviour
    {
        public virtual void Spawn(KartInventory kart, Direction direction)
        { }

        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (PhotonNetwork.connected)
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
            if (photonView.isMine)
            {
                PhotonNetwork.RemoveRPCs(photonView);
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}
