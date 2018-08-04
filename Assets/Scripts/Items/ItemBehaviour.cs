using UnityEngine;

namespace Items
{
    public class ItemBehaviour : MonoBehaviour
    {
        public virtual void Spawn(KartInventory kart, Directions direction)
        { }

        protected void DestroyObject()
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}