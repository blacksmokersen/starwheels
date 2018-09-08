using UnityEngine;

namespace Items
{
    public class ItemMineTrigger : MonoBehaviour
    {
        public bool Activated = false;

        private void OnTriggerEnter(Collider other)
        {
            if ((other.gameObject.CompareTag(Constants.Tag.DiskItem) ||
                other.gameObject.CompareTag(Constants.Tag.RocketItem))
                && Activated)
            {
                other.gameObject.GetComponentInParent<ProjectileBehaviour>().DestroyObject(); // Destroy the projectile
                GetComponentInParent<MineBehaviour>().PlayExplosion();
                GetComponentInParent<MineBehaviour>().DestroyObject(); // Destroy the mine root item
            }
        }
    }
}
