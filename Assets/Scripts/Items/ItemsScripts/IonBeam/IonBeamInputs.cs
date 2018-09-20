using UnityEngine;
using Items;
using CameraUtils;

namespace Controls
{
    public class IonBeamInputs : BaseKartComponent
    {
        public static bool IonBeamControlMode;

        private float _horizontalAxis;
        private float _verticalAxis;
        private PlayerInputs _playerinputs;
        private IonBeamBehaviour _ionBeamBehaviour;
        private IonBeamCamera _cinemachineDynamicScript;

        // CORE

        private new void Awake()
        {
            base.Awake();
            _cinemachineDynamicScript = GameObject.Find("PlayerCamera").GetComponent<IonBeamCamera>();
            _playerinputs = GetComponent<PlayerInputs>();
            _ionBeamBehaviour = GetComponent<IonBeamBehaviour>();
        }

        private void Update()
        {
            MoveCam();
            ButtonsDown();
        }

        // PUBLIC

        // PRIVATE

        private void MoveCam()
        {
            _horizontalAxis = Input.GetAxis(Constants.Input.UpAndDownAxis);
            _verticalAxis = Input.GetAxis(Constants.Input.TurnAxis);
            _cinemachineDynamicScript.IonBeamCameraControls(_horizontalAxis, _verticalAxis);
        }

        private void ButtonsDown()
        {
            if (Input.GetButtonDown(Constants.Input.UseItem))
            {
                kartEvents.OnIonBeamFire();
            }
        }
    }
}
