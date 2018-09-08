using UnityEngine;

namespace CameraUtils
{
    public class SpectatorControls : MonoBehaviour
    {
        public bool Enabled = false;

        private CameraPlayerSwitch _cameraPlayerSwitch;

        // CORE

        private void Awake()
        {
            _cameraPlayerSwitch = GetComponent<CameraPlayerSwitch>();
        }

        // PUBLIC

        // PRIVATE

        private void Update()
        {
            if (Enabled && Input.GetButtonDown(Constants.Input.SwitchCamToNextPlayer))
            {
                _cameraPlayerSwitch.SetCameraToNextPlayer();
            }
        }
    }
}
