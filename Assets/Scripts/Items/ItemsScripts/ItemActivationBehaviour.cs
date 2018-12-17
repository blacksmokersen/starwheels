using UnityEngine;

namespace Items
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public class ItemActivationBehaviour : MonoBehaviour
    {
        public bool Activated = false;

        private void OnTriggerExit(Collider other)
        {
            Activated = true;
        }
    }
}

