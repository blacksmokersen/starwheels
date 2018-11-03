using UnityEngine;

namespace KBA.Debug
{
    public class DebugTeleporter : MonoBehaviour, IControllable
    {
        [Header("Teleport Settings")]
        [SerializeField] private GameObject _teleportTarget;
        [SerializeField] private Vector3 _teleportPosition;

        // CORE

        void Update()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                _teleportTarget.transform.position = _teleportPosition;
            }
        }
    }
}
