using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Photon;

namespace Knockout
{
    public class Knockout : MonoBehaviour
    {
        [Header("Is Active")]
        [SerializeField] private bool Enabled = false;

        [Header("Events")]
        public UnityEvent OnKnockout;

        // PUBLIC

        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            Enabled = false;
        }

        public void ApplyKnockoutState()
        {
            if (Enabled)
            {
                Debug.Log("Applying knockout state");
                OnKnockout.Invoke();
            }
        }
    }
}
