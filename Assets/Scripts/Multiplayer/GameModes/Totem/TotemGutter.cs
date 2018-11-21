using UnityEngine;

namespace GameModes.Totem
{
    public class TotemGutter : MonoBehaviour
    {
        [SerializeField] private Vector3 _respawnPosition;

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.Totem) && !other.isTrigger) // Physical collider
            {
                if (BoltNetwork.isServer)
                {
                    Debug.Log("Totem fell in gutter.");
                    other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    other.GetComponent<TotemBehaviour>().SetParent(null);
                    other.GetComponent<TotemBehaviour>().SetTotemKinematic(false);
                    other.transform.position = _respawnPosition;
                    other.GetComponent<BoltEntity>().GetState<IItemState>().OwnerID = -1;
                }
            }
        }
    }
}
