using UnityEngine;
using Controls;
using CameraUtils;
using System.Collections;


namespace Items
{
    public class IonBeamBehaviour : ItemBehaviour
    {
        private IonBeamInputs _ionBeamInputs;
        private IonBeamCamera _ionBeamCam;
        private bool _isFiring = false;

        [Header("Sounds")]
        public AudioSource LaunchSource;

        private void Awake()
        {
            _ionBeamCam = GameObject.Find("PlayerCamera").GetComponent<IonBeamCamera>();
            _ionBeamInputs = GetComponent<IonBeamInputs>();
        }

        public override void Spawn(Inventory kart, Direction direction, float aimAxis)
        {
            _ionBeamCam.IonBeamCameraBehaviour(true);
            EnableIonInputs();
        }

        private void Update()
        {
            transform.position = _ionBeamCam.transposer.transform.position;
        }

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
                /*
                GameObject IonBeam = BoltNetwork.Instantiate("Items/" + "IonBeamLaser", new Vector3(camPosition.x, 0, camPosition.z), Quaternion.identity);
                IonBeam.transform.position = new Vector3(_ionBeamCam.transform.position.x, IonBeam.transform.position.y, _ionBeamCam.transform.position.z);
                MyExtensions.Audio.PlayClipObjectAndDestroy(LaunchSource);
                _ionBeamCam.composer.enabled = true;
                _ionBeamCam.IonBeamCameraBehaviour(false);
                */
                StartCoroutine(DelayBeforeInputsChange());
                _isFiring = true;
            }
        }

        IEnumerator DelayBeforeDisablePlayerInputs()
        {
            yield return new WaitForSeconds(1);
        }

        IEnumerator DelayBeforeInputsChange()
        {
            yield return new WaitForSeconds(1);
            _ionBeamCam.GetComponent<IonBeamCamera>().enabled = false;
            Destroy(gameObject);
        }
    }
}
