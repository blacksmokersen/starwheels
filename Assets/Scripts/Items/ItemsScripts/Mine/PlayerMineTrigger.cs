using UnityEngine;

namespace Items
{
    public class PlayerMineTrigger : MonoBehaviour
    {
        public bool Activated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag && Activated)
            {
                other.gameObject.GetComponentInParent<Kart.KartEvents>().OnHit();
                GetComponentInParent<MineBehaviour>().PlayExplosion();
                GetComponentInParent<MineBehaviour>().DestroyObject(); // Destroy the mine root item
            }
        }
    }
}
