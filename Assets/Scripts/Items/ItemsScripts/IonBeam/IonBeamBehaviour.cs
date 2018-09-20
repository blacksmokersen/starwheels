using UnityEngine;
using Controls;
using CameraUtils;
using Kart;
using Photon.Pun;
using System.Collections;


namespace Items
{
    public class IonBeamBehaviour : ItemBehaviour
    {
        private IonBeamInputs ionBeamInputs;
        public IonBeamCamera cam;
        private KartEvents kartEvents;
        private KartHub karthub;


        private void Awake()
        {
            cam = GameObject.Find("PlayerCamera").GetComponent<IonBeamCamera>();
            ionBeamInputs = GetComponent<IonBeamInputs>();
        }

        public override void Spawn(KartInventory kart, Direction direction, float aimAxis)
        {
            kartEvents = kart.GetComponentInParent<KartEvents>();
            kartEvents.OnIonBeamFire += FireIonBeam;
            cam.IonBeamCameraBehaviour(true);
            karthub = kart.GetComponentInParent<KartHub>();
            EnableIonInputs();
        }

        private void Update()
        {
            transform.position = cam.transposer.transform.position;
        }

        public void EnableIonInputs()
        {
            karthub.GetComponentInChildren<PlayerInputs>().enabled = false;
            cam.GetComponent<CinemachineDynamicScript>().enabled = false;
            cam.GetComponent<IonBeamCamera>().enabled = true;
            StartCoroutine(DelayBeforeInputsChange(false));
        }


        public void FireIonBeam()
        {
            Vector3 camPosition = cam.transposer.transform.position;
            GameObject IonBeam = PhotonNetwork.Instantiate("Items/" + "IonBeamLaser", new Vector3(camPosition.x, 0, camPosition.z), Quaternion.identity);
            //  IonBeam.transform.position = cam.transposer.transform.position;
            cam.composer.enabled = true;
            cam.IonBeamCameraBehaviour(false);
            StartCoroutine(DelayBeforeInputsChange(true));
        }

        IEnumerator DelayBeforeInputsChange(bool disable)
        {
            if (disable)
            {
                yield return new WaitForSeconds(1);
                karthub.GetComponentInChildren<PlayerInputs>().enabled = true;
                cam.GetComponent<CinemachineDynamicScript>().enabled = true;
                karthub.GetComponentInChildren<IonBeamInputs>().enabled = false;
                cam.GetComponent<IonBeamCamera>().enabled = false;
            }
            else
            {
                yield return new WaitForSeconds(1);
                karthub.GetComponentInChildren<IonBeamInputs>().enabled = true;
            }

        }
    }
}
