using UnityEngine;

namespace SW.DebugUtils
{
    public class DebugBoltEventsPanelOpener : MonoBehaviour
    {
        [SerializeField] private GameObject _debugPanel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _debugPanel.SetActive(!_debugPanel.activeInHierarchy);
            }
        }
    }
}
