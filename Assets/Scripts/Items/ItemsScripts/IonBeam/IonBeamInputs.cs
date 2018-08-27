using UnityEngine;
using Items;

namespace Controls
{
    public class IonBeamInputs : BaseKartComponent
    {
        public static bool IonBeamControlMode;
        private float horizontalAxis;
        private float verticalAxis;
        private PlayerInputs playerinputs;
        private IonBeamBehaviour ionBeamBehaviour;
        private CinemachineDynamicScript cinemachineDynamicScript;

        private new void Awake()
        {
            base.Awake();
            cinemachineDynamicScript = kartHub.GetComponent<CinemachineDynamicScript>();
            playerinputs = GetComponent<PlayerInputs>();
            ionBeamBehaviour = GetComponent<IonBeamBehaviour>();
        }

        private void Update()
        {
            MoveCam();
            ButtonsDown();
        }

        public void MoveCam()
        {
            horizontalAxis = Input.GetAxis(Constants.UpAndDownAxis);
            verticalAxis = Input.GetAxis(Constants.TurnAxis);
            if (IonBeamControlMode)
            {
                cinemachineDynamicScript.IonBeamCameraControls(horizontalAxis, verticalAxis);
            }
        }

        void ButtonsDown()
        {
            if (Input.GetButtonDown(Constants.UseItemButton))
            {
                if (IonBeamControlMode)
                {
                    ionBeamBehaviour.FireIonBeam();
                    DisableKartInputs(false);
                }
            }
        }

        public void DisableKartInputs(bool value)
        {
            IonBeamControlMode = false;
            playerinputs.Enabled = value;
        }
    }
}
