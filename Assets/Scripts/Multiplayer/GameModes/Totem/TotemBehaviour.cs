using System.Collections;
using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    public class TotemBehaviour : EntityBehaviour<IThrowableState>
    {
        [Header("Slowdown Settings")]
        [SerializeField] private float _slowdownFactor = 0.98f;
        [SerializeField] private float _stopMagnitudeThreshold = 0.1f;

        private Rigidbody _rb;
        private bool _isSlowingDown = false;

        // CORE

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_isSlowingDown)
            {
                _rb.velocity *= _slowdownFactor;
            }
            if(_rb.velocity.magnitude < _stopMagnitudeThreshold)
            {
                _isSlowingDown = false;
                _rb.velocity = Vector3.zero;
            }
        }

        // BOLT

        public override void ControlGained()
        {
            state.SetTransforms(state.Transform, transform);
        }

        // PUBLIC

        public void StartSlowdown()
        {
            StartCoroutine(SlowdownRoutine());
        }

        // PRIVATE

        private IEnumerator SlowdownRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            _isSlowingDown = true;
        }
    }
}
