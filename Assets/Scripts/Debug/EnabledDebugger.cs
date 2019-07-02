using UnityEngine;

namespace SW.DebugUtils
{
    [DisallowMultipleComponent]
    public class EnabledDebugger : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log(name + " : Enabled.");
        }

        private void OnDisable()
        {
            Debug.Log(name + " : Disabled.");
        }
    }
}
