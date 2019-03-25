using UnityEngine;
using Bolt;

namespace SW.DebugUtils
{
    public class DebugTeleporter : EntityBehaviour, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Teleport Settings")]
        [SerializeField] private GameObject _teleportTarget;
        [SerializeField] private Vector3 _teleportPosition;
        [SerializeField] private bool _teleportOnKartSpawner = false;

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled && Input.GetKeyDown(KeyCode.Alpha9))
            {
                if (_teleportOnKartSpawner)
                {
                    var spawnPosition = FindObjectOfType<Multiplayer.SpawnCaller>().transform;
                    _teleportTarget.transform.position = spawnPosition.position;
                    _teleportTarget.transform.rotation = spawnPosition.rotation;
                }
                else
                {
                    _teleportTarget.transform.position = _teleportPosition;
                }
                _teleportTarget.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
