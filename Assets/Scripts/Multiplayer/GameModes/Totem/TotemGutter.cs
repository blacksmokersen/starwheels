using UnityEngine;

namespace GameModes.Totem
{
    public class TotemGutter : MonoBehaviour
    {
        [SerializeField] private Vector3 respawnPosition;

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.Totem) && !other.isTrigger) // Physical collider
            {
                if (BoltNetwork.isServer)
                {
                    other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    other.gameObject.transform.position = respawnPosition;
                    other.gameObject.transform.SetParent(null);
                }
            }
        }
    }
}
