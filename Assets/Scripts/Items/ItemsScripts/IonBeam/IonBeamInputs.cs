using UnityEngine;
using Items;
using CameraUtils;
using Bolt;

namespace Controls
{
    public class IonBeamInputs : EntityBehaviour, IControllable
    {
        //public static bool IonBeamControlMode;

        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        private float _horizontalAxis;
        private float _verticalAxis;
        private IonBeamBehaviour _ionBeamBehaviour;
        private IonBeamCamera _ionBeamCamera;

        // CORE

        private void Awake()
        {
            _ionBeamCamera = GameObject.Find("IonBeamCamera").GetComponent<IonBeamCamera>();
            _ionBeamBehaviour = GetComponent<IonBeamBehaviour>();
        }

        private void Update()
        {
            if (entity.isControllerOrOwner)
            {
                MoveCam();
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled && Input.GetButtonDown(Constants.Input.UseItem) || Input.GetButtonDown(Constants.Input.UseItemForward))
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
