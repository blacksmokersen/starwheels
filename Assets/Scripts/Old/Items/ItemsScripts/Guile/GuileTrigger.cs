using UnityEngine;

namespace Items
{
    public class GuileTrigger : ProjectileTrigger
    {
        private new void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.gameObject.CompareTag(Constants.Tag.GroundItem) ||
                other.gameObject.CompareTag(Constants.Tag.DiskItem) ||
                other.gameObject.CompareTag(Constants.Tag.RocketItem))
            {
                other.gameObject.GetComponentInParent<ItemBehaviour>().DestroyObject();
            }
            else if (other.gameObject.CompareTag(Constants.Tag.GuileItem))
            {
                GetComponentInParent<ItemBehaviour>().DestroyObject();
            }
        }
    }
}
