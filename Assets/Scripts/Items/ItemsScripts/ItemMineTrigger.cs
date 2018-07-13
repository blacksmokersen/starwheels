using UnityEngine;

namespace Items
{
    public class ItemMineTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartTag)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
