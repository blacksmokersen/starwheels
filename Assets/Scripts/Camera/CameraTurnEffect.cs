using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class CameraTurnEffect : MonoBehaviour, IControllable
    {
        private CinemachineOrbitalTransposer _orbiter;
        private CinemachineVirtualCamera _cinemachine;

        private void Awake()
        {
            _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
            _orbiter = _cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        private void Start()
        {
            _orbiter.m_XAxis.m_InputAxisName = "RightJoystickHorizontal";
        }

        private void Update()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.ResetCamera))
            {
                CameraReset();
            }
            TurnCamera(Input.GetAxis(Constants.Input.TurnCamera));
        }

        // PRIVATE

        private void TurnCamera(float value)
        {
            if (Mathf.Abs(_orbiter.m_XAxis.Value) >= 1f)
                _orbiter.m_RecenterToTargetHeading.m_enabled = true;
            else
                _orbiter.m_RecenterToTargetHeading.m_enabled = false;
        }

        private void CameraReset()
        {
            _orbiter.m_XAxis.Value = 0;
        }
    }
}
