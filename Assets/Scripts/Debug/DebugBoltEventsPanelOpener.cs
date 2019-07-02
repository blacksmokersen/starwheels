using UnityEngine;

namespace SW.DebugUtils
{
    public class DebugBoltEventsPanelOpener : MonoBehaviour
    {
        [SerializeField] private GameObject _debugPanel;

        private bool _securityEngaged = true;

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightAlt) && Input.GetKeyDown(KeyCode.Equals))
            {
                _securityEngaged = !_securityEngaged;
            }

            if (!_securityEngaged && Input.GetKeyDown(KeyCode.Alpha0) && BoltNetwork.IsServer)
            {
                _debugPanel.SetActive(!_debugPanel.activeInHierarchy);
            }
        }
    }
}
