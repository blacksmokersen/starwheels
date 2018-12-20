using UnityEngine;
using Bolt;

namespace KBA.Debug
{
    public class DebugTeleporter : EntityBehaviour, IControllable
    {
        [Header("Teleport Settings")]
        [SerializeField] private GameObject _teleportTarget;
        [SerializeField] private Vector3 _teleportPosition;

        private void Update()
        {
            if (entity.isControllerOrOwner)
            {
                MapInputs();
            }
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
