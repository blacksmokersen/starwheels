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
                Destroy(gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("LLOLOLOLO");
            if (other.gameObject.tag == Constants.KartRigidBodyTag && Activated)
            {
                Destroy(gameObject);
            }
        }
    }
}
