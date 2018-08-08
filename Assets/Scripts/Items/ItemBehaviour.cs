using UnityEngine;

namespace Items
{
    public class ItemBehaviour : MonoBehaviour
    {
        public virtual void Spawn(KartInventory kart, Directions direction)
        { }

        public void DestroyObject()
        {
            PhotonView view = GetComponent<PhotonView>();
            if (PhotonNetwork.connected)
            {
                if (view.isMine)
                    PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}