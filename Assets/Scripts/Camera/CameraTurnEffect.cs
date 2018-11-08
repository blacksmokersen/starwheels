using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class CameraTurnEffect : MonoBehaviour, IControllable
    {
        private string _turnCamInputName = "RightJoystickHorizontal";
        private CinemachineOrbitalTransposer _orbiter;
        private CinemachineVirtualCamera _cinemachine;

        private float _controlAxisValueMin;
        private float _controlAxisValueMax;

        private void Awake()
        {
            _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
            _orbiter = _cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            _controlAxisValueMin = _orbiter.m_XAxis.m_MinValue;
            _controlAxisValueMax = _orbiter.m_XAxis.m_MaxValue;
        }

        private void Start()
        {
            _orbiter.m_XAxis.m_InputAxisName = _turnCamInputName;
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
            WhenToRecenterEnableCam(Input.GetAxis(Constants.Input.TurnCamera));
        }

        public void DisableTurnEffectInput()
        {
            _orbiter.m_XAxis.m_InputAxisName = "";
            _orbiter.m_XAxis.m_MinValue = 0;
            _orbiter.m_XAxis.m_MaxValue = 0;
        }

        public void EnableTurnEffectInput()
        {
            _orbiter.m_XAxis.m_InputAxisName = _turnCamInputName;
            _orbiter.m_XAxis.m_MinValue = _controlAxisValueMin;
            _orbiter.m_XAxis.m_MaxValue = _controlAxisValueMax;
        }

        public void CenterCamera()
        {
            _orbiter.m_XAxis.Value = 0;
        }

        // PRIVATE

        private void WhenToRecenterEnableCam(float value)
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
