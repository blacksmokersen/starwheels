using UnityEngine;
using Cinemachine;

namespace CameraUtils
{
    public class BackCamera : MonoBehaviour, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

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
            if (Enabled && Input.GetButtonDown(Constants.Input.BackCamera))
            {
                BackCameraSwitch(true);
            }

            if (Input.GetButtonUp(Constants.Input.BackCamera))
            {
                BackCameraSwitch(false);
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
