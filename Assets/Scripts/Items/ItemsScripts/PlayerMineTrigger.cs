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
                other.gameObject.GetComponentInParent<Kart.KartHealthSystem>().HealtLoss();
                Destroy(transform.parent.gameObject); // Destroy the mine root item
            }
        }
    }
}
