using UnityEngine;
using System.Collections;
using Cinemachine;

namespace CameraUtils
{
    public class CamSpeedEffect : MonoBehaviour, IObserver
    {
        [Header("Effect Settings")]
        [SerializeField] private float _fov = 50f;
        [SerializeField] private float _addedFovOnBoost = 5f;
        [SerializeField] private float _addedFovDuration = 5f;

        private CinemachineVirtualCamera _cinemachine;
        private Coroutine _fovBoostCoroutine;
        private Rigidbody _rigidbody;
        private Drift.Drift _drift;
        private float _addedFOVMax = 0;

        // CORE

        private void Awake()
        {
            _cinemachine = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            SpeedOnCamBehaviour();
        }

        // PUBLIC

        public void Observe(GameObject kartRoot)
        {
            _rigidbody = kartRoot.GetComponent<Rigidbody>();
            _drift = kartRoot.GetComponentInChildren<Drift.Drift>();
            _drift.OnDriftBoostStart.AddListener(UnlockFOV);
        }

        public void UnlockFOV()
        {
            if (_fovBoostCoroutine == null)
                _fovBoostCoroutine = StartCoroutine(UnlockFOVRoutine(_addedFovOnBoost, _addedFovDuration));
        }

        // PRIVATE

        private void SpeedOnCamBehaviour()
        {
            if (_rigidbody)
            {
                float clampCam = Mathf.Clamp(_rigidbody.velocity.magnitude / 2, 0, 20);
                _cinemachine.m_Lens.FieldOfView = _fov + clampCam + _addedFOVMax;
            }
        }

        private IEnumerator UnlockFOVRoutine(float value, float duration)
        {
            var _currentTimer = 0f;
            while (_currentTimer < duration / 2)
            {
                _addedFOVMax += value;
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            var _currentTimer2 = 0f;
            while (_currentTimer2 < duration / 2)
            {
                _addedFOVMax -= value;
                _currentTimer2 += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            _addedFOVMax = 0;
            _fovBoostCoroutine = null;
        }
    }
}
