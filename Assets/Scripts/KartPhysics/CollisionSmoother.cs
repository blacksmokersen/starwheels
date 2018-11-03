using UnityEngine;

namespace KartPhysics
{
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionSmoother : MonoBehaviour
    {
        [Header("Smooth Settings")]
        [SerializeField] private bool _smoothCollisionWithGround = false;
        [Tooltip("Value of 1f is really boucy, 0f means no smoothing.")]
        [SerializeField] private float _smoothFactor = 1f;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // PRIVATE

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                _rb.constraints = RigidbodyConstraints.FreezeRotationY;

                var collisionForce = collision.relativeVelocity.magnitude;
                var collisionNormal = collision.contacts[0].normal;
                if(!_smoothCollisionWithGround) collisionNormal.y = 0;

                _rb.AddForce(collisionNormal * collisionForce * _smoothFactor, ForceMode.VelocityChange);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                _rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
