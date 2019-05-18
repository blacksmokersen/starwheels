using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class CamSpeedEffect : MonoBehaviour, IObserver
    {
        [Header("Effect Settings")]
        [SerializeField] private float _fov = 50f;

        private CinemachineVirtualCamera _cinemachine;
        private Rigidbody _rigidbody;

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
        }

        // PRIVATE

        private void SpeedOnCamBehaviour()
        {
            if (_rigidbody)
            {
                float clampCam = Mathf.Clamp(_rigidbody.velocity.magnitude / 2, 0, 20);
                _cinemachine.m_Lens.FieldOfView = _fov + clampCam;

            }
        }
    }
}
