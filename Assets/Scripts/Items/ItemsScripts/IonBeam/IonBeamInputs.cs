using UnityEngine;
using Items;
using CameraUtils;
using Bolt;

namespace Controls
{
    public class IonBeamInputs : EntityBehaviour, IControllable
    {
        //public static bool IonBeamControlMode;

        private float _horizontalAxis;
        private float _verticalAxis;
        private IonBeamBehaviour _ionBeamBehaviour;
        private IonBeamCamera _ionBeamCamera;

        // CORE

        private void Awake()
        {
            _ionBeamCamera = GameObject.Find("PlayerCamera").GetComponent<IonBeamCamera>();
            _ionBeamBehaviour = GetComponent<IonBeamBehaviour>();
        }

        private void Update()
        {
            if (entity.isOwner)
            {
                MoveCam();
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseItem) || Input.GetButtonDown(Constants.Input.UseItemForward))
            {
                _ionBeamBehaviour.FireIonBeam();
            }
        }

        // PRIVATE

        private void MoveCam()
        {
            _horizontalAxis = Input.GetAxis(Constants.Input.UpAndDownAxis);
            _verticalAxis = Input.GetAxis(Constants.Input.TurnAxis);
            _ionBeamCamera.IonBeamCameraControls(_horizontalAxis, _verticalAxis);
        }
    }
}
