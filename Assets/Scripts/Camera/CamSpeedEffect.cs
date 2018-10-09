using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class CamSpeedEffect : MonoBehaviour
    {

        private CinemachineVirtualCamera _cinemachine;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _cinemachine = GetComponent<CinemachineVirtualCamera>();
        }

        void Update()
        {
            SpeedOnCamBehaviour();
        }

        public void SpeedOnCamBehaviour()
        {
            if (_rigidbody)
            {
                float clampCam = Mathf.Clamp(_rigidbody.velocity.magnitude / 5, 0, 20);
                _cinemachine.m_Lens.FieldOfView = 50 + clampCam;
            }
        }

        public void SetRigidbody(Rigidbody rb)
        {
            _rigidbody = rb;
        }
    }
}
