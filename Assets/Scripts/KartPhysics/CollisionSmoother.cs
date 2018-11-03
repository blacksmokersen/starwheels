using System.Collections;
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

        private Coroutine _stuckCoroutine;
        private bool _isStuck = false;
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
                _stuckCoroutine = StartCoroutine(StuckPrevention());
                _rb.constraints = RigidbodyConstraints.FreezeRotationY;
                BounceOut(collision);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                if (_isStuck)
                {
                    BounceOut(collision, 8f);
                    Debug.Log("Bouncing out of of stuck state.");
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                _rb.constraints = RigidbodyConstraints.None;

                if(_stuckCoroutine != null)
                {
                    StopCoroutine(_stuckCoroutine);
                    _isStuck = false;
                }
            }
        }

        private void BounceOut(Collision collision, float additionalBouceForce = 0f)
        {
            var collisionForce = collision.relativeVelocity.magnitude + additionalBouceForce;
            var collisionNormal = collision.contacts[0].normal;
            if (!_smoothCollisionWithGround) collisionNormal.y = 0;

            _rb.AddForce(collisionNormal * collisionForce * _smoothFactor, ForceMode.VelocityChange);
        }

        private IEnumerator StuckPrevention()
        {
            yield return new WaitForSeconds(2f);
            _isStuck = true;
        }
    }
}
