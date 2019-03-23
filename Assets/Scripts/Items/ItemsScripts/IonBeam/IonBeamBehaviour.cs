using UnityEngine;
using Controls;
using CameraUtils;
using System.Collections;
using Steering;
using Engine;
using Bolt;

namespace Items
{
    public class IonBeamBehaviour : EntityBehaviour<IItemState>
    {
        [SerializeField] private GameObject ionBeamLaserPrefab;

        private GameObject _ionBeamOwner;
        private GameObject _playerCamera;
        private IonBeamInputs _ionBeamInputs;
        private IonBeamCamera _ionBeamCam;
        private bool _isFiring = false;

        [HideInInspector] public bool IsFiringBackward = false;
        public string OwnerNickname;
        public string ItemName;

        //CORE

        private void Awake()
        {
            _ionBeamCam = GameObject.Find("IonBeamCamera").GetComponent<IonBeamCamera>();
            _playerCamera = GameObject.Find("PlayerCamera");
            _ionBeamInputs = GetComponent<IonBeamInputs>();
        }

        public void Start()
        {
            _ionBeamOwner = GetComponent<Ownership>().OwnerKartRoot;
            if (entity.isOwner)
            {
                if (IsFiringBackward)
                    LaunchImmediateIonBeamBackwards(_ionBeamOwner.transform.position);
                else
                {
                    _ionBeamCam.GetIonBeamBehaviour(this);
                    _ionBeamCam.IonBeamCameraBehaviour(true);
                    EnableIonInputs();
                }
            }
        }

        public override void SimulateController()
        {
            transform.position = _ionBeamCam.transform.position;
        }

        //PUBLIC

        public void EnableIonInputs()
        {
            _ionBeamCam.GetComponent<IonBeamCamera>().enabled = true;
            _ionBeamCam.ResetCameraTransform();
            _ionBeamCam.GetComponentInChildren<Camera>().enabled = true;
            _playerCamera.GetComponent<CameraTurnEffect>().DisableTurnEffectInput();
            _playerCamera.GetComponent<CameraTurnEffect>().CenterCamera();
            StartCoroutine(DelayBeforeDisablePlayerInputs());
        }

        public void DisableIonBeam()
        {
            _ionBeamCam.GetComponent<IonBeamCamera>().enabled = false;
            _ionBeamCam.GetComponentInChildren<Camera>().enabled = false;
            _ionBeamInputs.enabled = false;
            _ionBeamOwner.GetComponentInChildren<SteeringWheel>().CanSteer = true;
            _ionBeamOwner.GetComponentInChildren<EngineBehaviour>().enabled = true;
            _playerCamera.GetComponent<CameraTurnEffect>().EnableTurnEffectInput();
            BoltNetwork.Destroy(gameObject);
        }

        public void FireIonBeam()
        {
            if (!_isFiring && entity.isOwner)
            {
                Vector3 camPosition = _ionBeamCam.transform.position;
                _ionBeamCam.GetComponent<IonBeamCamera>().enabled = false;
                _ionBeamCam.GetComponentInChildren<Camera>().enabled = false;

                var IonBeam = BoltNetwork.Instantiate(ionBeamLaserPrefab, new Vector3(camPosition.x, 0, camPosition.z), Quaternion.identity);
                var itemState = IonBeam.GetComponent<BoltEntity>().GetState<IItemState>();

                itemState.Team = state.Team;
                itemState.OwnerID = state.OwnerID;
                itemState.OwnerNickname = OwnerNickname;
                itemState.Name = ItemName;

                IonBeam.transform.position = new Vector3(_ionBeamCam.transform.position.x, IonBeam.transform.position.y, _ionBeamCam.transform.position.z);
                _ionBeamCam.IonBeamCameraBehaviour(false);
                if (entity.isOwner)
                    StartCoroutine(DelayBeforeInputsChange());
                _isFiring = true;
            }
        }

        public void LaunchImmediateIonBeamBackwards(Vector3 position)
        {
            var IonBeam = BoltNetwork.Instantiate(ionBeamLaserPrefab, position, Quaternion.identity);
            var itemState = IonBeam.GetComponent<BoltEntity>().GetState<IItemState>();

            itemState.Team = state.Team;
            itemState.OwnerID = state.OwnerID;
            itemState.OwnerNickname = OwnerNickname;
            itemState.Name = ItemName;

            IonBeam.transform.position = position;
        }

        //PRIVATE

        IEnumerator DelayBeforeDisablePlayerInputs()
        {
            _ionBeamOwner.GetComponentInChildren<SteeringWheel>().CanSteer = false;
            _ionBeamOwner.GetComponentInChildren<SteeringWheel>().ResetAxisValue();
            _ionBeamOwner.GetComponentInChildren<EngineBehaviour>().enabled = false;
            while (!_ionBeamCam.IsCameraOnTop())
            {
                yield return new WaitForSeconds(0.1f);
            }
            _ionBeamInputs.enabled = true;
        }

        IEnumerator DelayBeforeInputsChange()
        {
            yield return new WaitForSeconds(1);
         //   _ionBeamCam.GetComponent<IonBeamCamera>().enabled = false;
         //   _ionBeamCam.GetComponentInChildren<Camera>().enabled = false;
            _ionBeamInputs.enabled = false;
            _ionBeamOwner.GetComponentInChildren<SteeringWheel>().CanSteer = true;
            _ionBeamOwner.GetComponentInChildren<EngineBehaviour>().enabled = true;
            _playerCamera.GetComponent<CameraTurnEffect>().EnableTurnEffectInput();
            BoltNetwork.Destroy(gameObject);
        }
    }
}
