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
                    RespawnTotem();
                }
                else
                {
                    var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
                    totem.GetComponent<BoltEntity>().ReleaseControl();
                    totem.GetComponent<TotemBehaviour>().SetParent(null);
                    totem.GetComponent<TotemBehaviour>().SetTotemKinematic(false);
                }
            }
        }

        // PUBLIC

        public void RespawnTotem()
        {
            var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
            totem.GetComponent<Rigidbody>().velocity = Vector3.zero;
            totem.GetComponent<TotemBehaviour>().SetParent(null);
            totem.GetComponent<TotemBehaviour>().SetTotemKinematic(false);
            totem.transform.position = _respawnPosition;

            TotemThrown totemThrownEvent = TotemThrown.Create();
            totemThrownEvent.OwnerID = -1;
            totemThrownEvent.Send();

            totem.GetComponent<BoltEntity>().GetState<IItemState>().OwnerID = -1;
        }
    }
}
