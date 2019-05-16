using UnityEngine;

namespace Common.PhysicsUtils
{
    [RequireComponent(typeof(Rigidbody))]
    public class Hovering : MonoBehaviour
    {
        [SerializeField] private string _groundLayerName;
        [SerializeField] private FloatVariable _hoveringHeight;

        private Rigidbody _rb;
        private bool _enabled = true;

        // CORE

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            CheckGrounded();
        }

        // PUBLIC

        public void Enable()
        {
            _rb.useGravity = true;
            _enabled = true;
        }

        public void Disable()
        {
            _rb.useGravity = true;
            _enabled = false;
        }

        // PRIVATE

        private void CheckGrounded()
        {
            if (_enabled)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, _hoveringHeight.Value, 1 << LayerMask.NameToLayer(_groundLayerName)))
                {
                    _rb.useGravity = false;
                    _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                    transform.position = new Vector3(transform.position.x, hit.point.y + _hoveringHeight.Value, transform.position.z);
                }
                else
                {
                    _rb.useGravity = true;
                }
            }
        }
    }
}
