using UnityEngine;

namespace CameraUtilities
{
    public class SpectatorControls : MonoBehaviour
    {
        public bool Enabled = false;

        private CameraPlayerSwitch _cameraPlayerSwitch;

        private void Awake()
        {
            _cameraPlayerSwitch = GetComponent<CameraPlayerSwitch>();
        }

        private void Update()
        {
            if (Enabled)
            {
                if (Input.GetButtonDown(Constants.SwitchCamToNextPlayerButton))
                {
                    _cameraPlayerSwitch.SetCameraToNextPlayer();
                }
            }
        }
    }
}
