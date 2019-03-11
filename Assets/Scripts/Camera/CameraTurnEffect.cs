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
        private CinemachineCollider _cinemachineCollider;

        private float _controlAxisValueMin;
        private float _controlAxisValueMax;
        private bool _alreadyRecentered = false;

        private void Awake()
        {
            _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
            _orbiter = _cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            _composer = _cinemachine.GetCinemachineComponent<CinemachineComposer>();
            _cinemachineCollider = GetComponent<CinemachineCollider>();


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

                ClampXMaxAngle(Input.GetAxis(Constants.Input.TurnCamera), Input.GetAxis(Constants.Input.UpAndDownCamera));

                CamYMovements(Input.GetAxis(Constants.Input.UpAndDownCamera));
                //  WhenToRecenterEnableCam(Input.GetAxis(Constants.Input.TurnCamera));
            }
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

        public void CenterOrbiter()
        {
            _cinemachineCollider.enabled = true;
            _orbiter.m_FollowOffset.z = -6.35f;
            _orbiter.m_FollowOffset.y = 2.2f;
            _orbiter.m_FollowOffset.x = 0f;
            _alreadyRecentered = true;
        }


        // PRIVATE



        private void CamYMovements(float yAxisValue)
        {

            if (yAxisValue > 0.1f)
                _composer.m_TrackedObjectOffset.y = yAxisValue * 2;
            else if (yAxisValue < 0.1f)
                _composer.m_TrackedObjectOffset.y = yAxisValue * 4;

            if (yAxisValue >= 0.1f)
            {
                _alreadyRecentered = false;
                if (_orbiter.m_FollowOffset.z <= -5)
                    _orbiter.m_FollowOffset.z += 0.2f;
            }
            else if (yAxisValue <= -0.1f)
            {
                _alreadyRecentered = false;
                _cinemachineCollider.enabled = false;
                if (_orbiter.m_FollowOffset.z <= -0.9f)
                {
                    _orbiter.m_FollowOffset.z += 0.4f;
                    _orbiter.m_FollowOffset.y += 0.4f;
                }
            }
            else
            {
                if (!_alreadyRecentered)
                {
                    CenterOrbiter();
                }
            }


            /*
            if (yAxisValue >= 0.1f)
            {
                if (Mathf.Abs(_composer.m_TrackedObjectOffset.y) <= 3)
                    _composer.m_TrackedObjectOffset.y += 0.2f;
            }
            else if (yAxisValue <= -0.1f)
            {
                if (Mathf.Abs(_composer.m_TrackedObjectOffset.y) <= 3)
                    _composer.m_TrackedObjectOffset.y -= 0.2f;
            }
            else
            {
                _composer.m_TrackedObjectOffset.y = 0;
            }

            _orbiter.m_XAxis.m_MinValue = -Mathf.Abs(yAxisValue) * 125;
            _orbiter.m_XAxis.m_MaxValue = Mathf.Abs(yAxisValue) * 125;

            if (Mathf.Abs(yAxisValue) <= 0.3f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 600;
            }
            else if (Mathf.Abs(yAxisValue) <= 0.8f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 200;
            }
            else if (Mathf.Abs(yAxisValue) <= 1f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 800;
            }
            */
        }

        private void ClampXMaxAngle(float xAxisValue, float yAxisValue)
        {
            /*
            if (xAxisValue <= 0.2f || yAxisValue <= 0.3f)
            {
                _orbiter.m_XAxis.Value = 0;
            }
            */
            _orbiter.m_XAxis.m_MinValue = -Mathf.Abs(xAxisValue) * 125;
            _orbiter.m_XAxis.m_MaxValue = Mathf.Abs(xAxisValue) * 125;

            if (Mathf.Abs(xAxisValue) <= 0.3f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 600;
            }
            else if (Mathf.Abs(xAxisValue) <= 0.8f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 200;
            }
            else if (Mathf.Abs(xAxisValue) <= 1f)
            {
                _orbiter.m_XAxis.m_MaxSpeed = 800;
            }

            #region OldMethods

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


            /*
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
            */

            #endregion

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
