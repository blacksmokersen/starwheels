using UnityEngine;
using Controls;
using CameraUtils;
using System.Collections;
using Steering;
using Engine;
using Bolt;

namespace Items
{
    public class IonBeamBehaviour : EntityBehaviour
    {
        [SerializeField] private GameObject ionBeamLaserPrefab;

        private GameObject _ionBeamOwner;
        private IonBeamInputs _ionBeamInputs;
        private IonBeamCamera _ionBeamCam;
        private bool _isFiring = false;

        [Header("Sounds")]
        public AudioSource LaunchSource;

        //CORE

        private void Awake()
        {
            _ionBeamCam = GameObject.Find("PlayerCamera").GetComponent<IonBeamCamera>();
            _ionBeamInputs = GetComponent<IonBeamInputs>();
        }

        public void Start()
        {
            _ionBeamOwner = GetComponent<Ownership>().OwnerKartRoot;

            if (entity.isOwner)
            {
                _ionBeamCam.IonBeamCameraBehaviour(true);
                EnableIonInputs();
            }
        }

        private void Update()
        {
            transform.position = _ionBeamCam.transposer.transform.position;
        }

        //PUBLIC

        public void EnableIonInputs()
        {
            _ionBeamCam.GetComponent<IonBeamCamera>().enabled = true;
            StartCoroutine(DelayBeforeDisablePlayerInputs());
        }

        public void FireIonBeam()
        {
            if (!_isFiring)
            {
                Vector3 camPosition = _ionBeamCam.transposer.transform.position;

                var IonBeam = BoltNetwork.Instantiate(ionBeamLaserPrefab, new Vector3(camPosition.x, 0, camPosition.z), Quaternion.identity);

                Ownership IonOwnership = GetComponent<Ownership>();
                Ownership itemOwnership = IonBeam.GetComponent<Ownership>();

                itemOwnership.OwnerKartRoot = IonOwnership.OwnerKartRoot;
                itemOwnership.Team = IonOwnership.Team;

                IonBeam.transform.position = new Vector3(_ionBeamCam.transform.position.x, IonBeam.transform.position.y, _ionBeamCam.transform.position.z);
                //  MyExtensions.Audio.PlayClipObjectAndDestroy(LaunchSource);
                _ionBeamCam.composer.enabled = true;
                _ionBeamCam.IonBeamCameraBehaviour(false);

                StartCoroutine(DelayBeforeInputsChange());
                _isFiring = true;
            }
        }

        //PRIVATE

        IEnumerator DelayBeforeDisablePlayerInputs()
        {
            yield return new WaitForSeconds(1);
            _ionBeamOwner.GetComponentInChildren<SteeringWheel>().enabled = false;
            _ionBeamOwner.GetComponentInChildren<EngineBehaviour>().enabled = false;
            _ionBeamInputs.enabled = true;
        }

        IEnumerator DelayBeforeInputsChange()
        {
            yield return new WaitForSeconds(1);
            _ionBeamCam.GetComponent<IonBeamCamera>().enabled = false;
            _ionBeamInputs.enabled = false;
            _ionBeamOwner.GetComponentInChildren<SteeringWheel>().enabled = true;
            _ionBeamOwner.GetComponentInChildren<EngineBehaviour>().enabled = true;
            BoltNetwork.Destroy(gameObject);
        }
    }
}
