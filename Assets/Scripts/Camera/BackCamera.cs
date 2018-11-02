using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class BackCamera : MonoBehaviour, IControllable
    {
        private CinemachineTransposer _transposer;
        private CinemachineVirtualCamera _cinemachine;
        private bool _backCamActivated = false;

        private void Awake()
        {
            _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
            _transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void Update()
        {
            MapInputs();
        }

        public void BackCameraSwitch(bool activate)
        {
            if (activate)
            {
                _transposer.m_FollowOffset.z = 9;
                _backCamActivated = true;
            }
            else
            {
                _transposer.m_FollowOffset.z = -8.5f;
                _backCamActivated = false;
            }
        }

        #region MapInput
        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.BackCamera))
            {
                BackCameraSwitch(true);
                //  kartEvents.OnBackCameraStart(true);
            }

            if (Input.GetButtonUp(Constants.Input.BackCamera))
            {
                BackCameraSwitch(false);
                // kartEvents.OnBackCameraEnd(false);
            }
        }
        #endregion
    }
}
