using UnityEngine;
using Controls;

namespace Items
{
    public class IonBeamBehaviour : ItemBehaviour
    {
        public IonBeamInputs ionBeamInputs;
        public CinemachineDynamicScript cam;

        private void Awake()
        {
          //  ionBeamInputs = GetComponent<IonBeamInputs>();
        }

        private void Update()
        {
           // transform.position = cam.transposer.transform.position;
        }

        void Start()
        {
           // ionBeamInputs.DisableKartInputs(true);
            cam.IonBeamCameraBehaviour(true);
        }


        public void FireIonBeam()
        {
            Vector3 camPosition = cam.transposer.transform.position;
            GameObject IonBeam = Instantiate(Resources.Load("IonBeamLaser"), new Vector3 (camPosition.x, 0, camPosition.z), Quaternion.identity) as GameObject;
           // IonBeam.transform.position = cam.transposer.transform.position;
            cam.IonBeamCameraBehaviour(false);
        }




























        /*
        public override void Spawn(KartInventory kart, Directions direction)
        {

        }
        */

    }
}
