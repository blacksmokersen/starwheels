using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class BackCamera : MonoBehaviour, IControllable
    {
        [SerializeField] CameraSettings _cameraSettings;
        [SerializeField] float _backCamZPosition;

        private CinemachineTransposer _transposer;
        private CinemachineVirtualCamera _cinemachine;

        // CORE

        private void Awake()
        {
            _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
            _transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void Update()
        {
            MapInputs();
        }

        // PUBLIC

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

        public void BackCameraSwitch(bool activate)
        {
            if (activate)
            {
                _transposer.m_FollowOffset.z = _backCamZPosition;
            }
            else
            {
                _transposer.m_FollowOffset.z = _cameraSettings.BaseCamPosition.z;
            }
        }
    }
}
