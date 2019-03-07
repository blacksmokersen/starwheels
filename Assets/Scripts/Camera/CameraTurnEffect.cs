using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class CameraTurnEffect : MonoBehaviour, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        private string _turnCamInputName = "RightJoystickHorizontal";
        private CinemachineOrbitalTransposer _orbiter;
        private CinemachineComposer _composer;
        private CinemachineVirtualCamera _cinemachine;

        private float _controlAxisValueMin;
        private float _controlAxisValueMax;

        private void Awake()
        {
            _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
            _orbiter = _cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            _composer = _cinemachine.GetCinemachineComponent<CinemachineComposer>();
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
            if (Enabled)
            {
                if (Input.GetButtonDown(Constants.Input.ResetCamera))
                {
                    CameraReset();
                }

                ClampMaxAngle(Input.GetAxis(Constants.Input.TurnCamera));

                if (Input.GetAxis(Constants.Input.UpAndDownCamera) >= 0.1f)
                {
                    if (Mathf.Abs(_composer.m_TrackedObjectOffset.y) <= 3)
                        _composer.m_TrackedObjectOffset.y += 0.2f;
                }
                else if (Input.GetAxis(Constants.Input.UpAndDownCamera) <= -0.1f)
                {
                    if (Mathf.Abs(_composer.m_TrackedObjectOffset.y) <= 3)
                        _composer.m_TrackedObjectOffset.y -= 0.2f;
                }
                else
                {
                    _composer.m_TrackedObjectOffset.y = 0;
                }

                WhenToRecenterEnableCam(Input.GetAxis(Constants.Input.TurnCamera));
            }
        }

        public void DisableTurnEffectInput()
        {
            //  _orbiter.m_XAxis.m_InputAxisName = "";
            //  _orbiter.m_XAxis.m_MinValue = 0;
            //  _orbiter.m_XAxis.m_MaxValue = 0;
        }

        public void EnableTurnEffectInput()
        {
            //  _orbiter.m_XAxis.m_InputAxisName = _turnCamInputName;
            //  _orbiter.m_XAxis.m_MinValue = _controlAxisValueMin;
            //  _orbiter.m_XAxis.m_MaxValue = _controlAxisValueMax;
        }

        public void CenterCamera()
        {
            _orbiter.m_XAxis.Value = 0;
        }

        // PRIVATE


        private void ClampMaxAngle(float xAxisValue)
        {
            // _orbiter.m_XAxis.m_InputAxisValue = 80;

            /*
            if (xAxisValue < 0)
            {
                --_orbiter.m_XAxis.Value;
            }
            else if (xAxisValue > 0f)
            {
                ++_orbiter.m_XAxis.Value;
            }
            */

            if (Mathf.Abs(xAxisValue) <= 0.3f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 600;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.8f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 300;
            }
            else if (Mathf.Abs(xAxisValue) <= 1f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 800;
            }




            if (Mathf.Abs(xAxisValue) <= 0.1f)
            {
                _orbiter.m_XAxis.m_MinValue = -5;
                _orbiter.m_XAxis.m_MaxValue = 5;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.1f)
            {
                _orbiter.m_XAxis.m_MinValue = -10;
                _orbiter.m_XAxis.m_MaxValue = 10;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.2f)
            {
                _orbiter.m_XAxis.m_MinValue = -20;
                _orbiter.m_XAxis.m_MaxValue = 20;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.3f)
            {
                _orbiter.m_XAxis.m_MinValue = -30;
                _orbiter.m_XAxis.m_MaxValue = 30;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.4f)
            {
                _orbiter.m_XAxis.m_MinValue = -40;
                _orbiter.m_XAxis.m_MaxValue = 40;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.5f)
            {
                _orbiter.m_XAxis.m_MinValue = -55;
                _orbiter.m_XAxis.m_MaxValue = 55;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.6f)
            {
                _orbiter.m_XAxis.m_MinValue = -75;
                _orbiter.m_XAxis.m_MaxValue = 75;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.7f)
            {
                _orbiter.m_XAxis.m_MinValue = -95;
                _orbiter.m_XAxis.m_MaxValue = 95;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.8f)
            {
                _orbiter.m_XAxis.m_MinValue = -105;
                _orbiter.m_XAxis.m_MaxValue = 105;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.9f)
            {
                _orbiter.m_XAxis.m_MinValue = -115;
                _orbiter.m_XAxis.m_MaxValue = 115;
            }
            else if (Mathf.Abs(xAxisValue) <= 1f)
            {
                _orbiter.m_XAxis.m_MinValue = -125;
                _orbiter.m_XAxis.m_MaxValue = 125;
            }



            /*
            if (xAxisValue>=0.5f)
            {
            _orbiter.m_XAxis.m_MinValue = -Mathf.Abs(xAxisValue) * 125;
            _orbiter.m_XAxis.m_MaxValue = Mathf.Abs(xAxisValue) * 125;
            }
            */
        }

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
