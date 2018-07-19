using UnityEngine;

namespace Items
{
    public class ItemMineTrigger : MonoBehaviour
    {
        public bool Activated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.ProjectileTag && Activated)
            {
                Destroy(other.gameObject);
                Destroy(transform.parent.gameObject); // Destroy the mine root item
            }
        }
    }
}
