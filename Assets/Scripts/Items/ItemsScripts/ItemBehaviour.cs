using System.Collections;
using UnityEngine;

namespace Items
{
    public class ItemBehaviour : MonoBehaviour
    {
        public virtual void Spawn(KartInventory kart, Direction direction)
        { }

        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (PhotonNetwork.connected)
            {
                if (timeBeforeDestroy > 0)
                    StartCoroutine(DelayedDestroy(timeBeforeDestroy));
                else
                {
                    if (GetComponent<PhotonView>().isMine)
                        PhotonNetwork.Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject, timeBeforeDestroy);
            }
        }

        IEnumerator DelayedDestroy(float t)
        {
            yield return new WaitForSeconds(t);
            if (GetComponent<PhotonView>().isMine)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
