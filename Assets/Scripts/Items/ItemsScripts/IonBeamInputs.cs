using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Controls
{
    public class IonBeamInputs : MonoBehaviour
    {
        public static bool IonBeamControlMode;
        private float horizontalAxis;
        private float verticalAxis;
        private PlayerInputs playerinputs;
        private IonBeamBehaviour ionBeamBehaviour;
        private CinemachineDynamicScript cam;

        private void Awake()
        {
            //TODO Implementation apres refactoring
            cam = GameObject.Find("CM vcam1").GetComponent<CinemachineDynamicScript>();
            playerinputs = GameObject.Find("Inputs").GetComponent<PlayerInputs>();
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
                cam.IonBeamCameraControls(horizontalAxis, verticalAxis);
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
            if (value)
            {
                playerinputs.disableMovements = true;
                playerinputs.disableUseItem = true;
            }
            else
            {
                IonBeamControlMode = false;
                playerinputs.disableMovements = false;
                playerinputs.disableUseItem = false;
            }
        }
    }
}