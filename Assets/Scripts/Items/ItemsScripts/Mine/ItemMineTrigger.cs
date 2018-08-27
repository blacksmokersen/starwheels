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
                other.gameObject.GetComponent<ProjectileBehaviour>().DestroyObject(); // Destroy the projectile
                GetComponentInParent<MineBehaviour>().PlayExplosion();
                GetComponentInParent<MineBehaviour>().DestroyObject(); // Destroy the mine root item
            }
        }
    }
}
