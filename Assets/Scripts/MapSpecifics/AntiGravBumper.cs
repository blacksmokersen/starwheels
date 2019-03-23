using UnityEngine;
using UnityEngine.Events;

namespace MapsSpecifics
{
    [RequireComponent(typeof(Collider))]
    public class AntiGravBumper : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnBumped;

        [SerializeField] private FloatVariable _impulseForce;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.Kart))
            {
                other.GetComponent<Rigidbody>().AddForce(_impulseForce.Value * Vector3.up, ForceMode.Impulse);

                if (OnBumped != null)
                {
                    OnBumped.Invoke();
                }
            }
        }
    }
}
