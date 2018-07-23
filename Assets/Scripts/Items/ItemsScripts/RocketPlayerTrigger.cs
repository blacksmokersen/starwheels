using UnityEngine;

namespace Items {
    [RequireComponent(typeof(Collider))]
    public class RocketPlayerTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartTag)
            {
                Destroy(gameObject);
            }
        }
    }
}
