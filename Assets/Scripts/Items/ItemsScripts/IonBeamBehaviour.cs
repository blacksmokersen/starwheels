using UnityEngine;
using Controls;

namespace Items
{
    public class IonBeamBehaviour : ItemBehaviour
    {
        private IonBeamInputs ionBeamInputs;
        private CinemachineDynamicScript cam;

        private void Awake()
        {
            cam = GameObject.Find("CM vcam1").GetComponent<CinemachineDynamicScript>();
            ionBeamInputs = GetComponent<IonBeamInputs>();
        }


        void Start()
        {
            ionBeamInputs.DisableKartInputs(true);
            cam.IonBeamCameraBehaviour(true);
        }


        public void FireIonBeam()
        {
            Debug.Log("FIRE");
            cam.IonBeamCameraBehaviour(false);
        }




























        /*
        public override void Spawn(KartInventory kart, Directions direction)
        {

        }
        */

    }
}
