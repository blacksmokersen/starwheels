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
        private CinemachineDynamicScript _cinemachineDynamicScript;

        // CORE

        private new void Awake()
        {
            base.Awake();
            _cinemachineDynamicScript = kartHub.GetComponent<CinemachineDynamicScript>();
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

            if (IonBeamControlMode)
            {
                _cinemachineDynamicScript.IonBeamCameraControls(_horizontalAxis, _verticalAxis);
            }
        }

        private void ButtonsDown()
        {
            if (Input.GetButtonDown(Constants.Input.UseItem))
            {
                if (IonBeamControlMode)
                {
                    _ionBeamBehaviour.FireIonBeam();
                    DisableKartInputs(false);
                }
            }
        }

        public void DisableKartInputs(bool value)
        {
            IonBeamControlMode = false;
            _playerinputs.Enabled = value;
        }
    }
}
