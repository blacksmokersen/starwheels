using UnityEngine;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    public class TotemReleaseOnHit : MonoBehaviour
    {
        private void OnDestroy()
        {
            if (transform.childCount == 1) // Totem is set
            {
                Debug.LogError("Unsetting totem");
                var totem = transform.GetChild(0);
                totem.GetComponent<TotemBehaviour>().UnsetParent();
            }
        }
    }
}
