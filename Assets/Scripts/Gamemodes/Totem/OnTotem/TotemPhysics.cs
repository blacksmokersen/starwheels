using System.Collections;
using UnityEngine;

namespace Gamemodes.Totem
{
    [DisallowMultipleComponent]
    public class TotemPhysics : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;

        [Header("Physics Elements")]
        [SerializeField] private Collider _collider;

        private Rigidbody _rb;
        private bool _isSlowingDown = false;
        private Coroutine _slowdownCoroutine;

        // CORE

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_isSlowingDown)
            {
                Slowdown();
            }
        }

        // PUBLIC

        public void SetVelocityToZero()
        {
            _rb.velocity = Vector3.zero;
        }

        public void EnableCollider(bool b)
        {
            _collider.enabled = b;
            _rb.isKinematic = b;
        }

        public void StartSlowdown()
        {
            _slowdownCoroutine = StartCoroutine(SlowdownRoutine());
        }

        public void StopSlowdown()
        {
            if (_slowdownCoroutine != null)
            {
                StopCoroutine(_slowdownCoroutine);
            }
            _isSlowingDown = false;
        }

        // PRIVATE

        private IEnumerator SlowdownRoutine()
        {
            yield return new WaitForSeconds(_totemSettings.SecondsBeforeSlowdown);
            _isSlowingDown = true;
        }

        private void Slowdown()
        {
            _rb.velocity *= _totemSettings.SlowdownFactor;

            if (_rb.velocity.magnitude < _totemSettings.StopMagnitudeThreshold)
            {
                _isSlowingDown = false;
                _rb.velocity = Vector3.zero;
            }
        }
    }
}
