using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{

    [RequireComponent(typeof(Inventory))]
    public class KartEmissiveDisplayer : MonoBehaviour
    {

        [Header("Materials Emissives")]
        public Material GreenMaterial;
        public Material PurpleMaterial;
        public Material GoldMaterial;
        public Material DefaultMaterial;

        public GameObject Emissive;

        private string _emissiveToDisplay;
        private Inventory _inventory;


        public void Awake()
        {
            _inventory = GetComponent<Inventory>();
            Emissive.GetComponent<MeshRenderer>();

        }

        

        public void DisplayEmissive()
        {
            if (_inventory.CurrentItem != null)
            {
                _emissiveToDisplay = _inventory.CurrentItem.Name;

                switch (_emissiveToDisplay)
                {

                    case "Disk":
                        Emissive.GetComponent<MeshRenderer>().material = GreenMaterial;
                        break;


                    case "Mine":
                        Emissive.GetComponent<MeshRenderer>().material = GreenMaterial;
                        break;

                    case "Guile":
                        Emissive.GetComponent<MeshRenderer>().material = GreenMaterial;
                        break;

                    case "Rocket":
                        Emissive.GetComponent<MeshRenderer>().material = PurpleMaterial;
                        break;


                    case "IonBeam":
                        Emissive.GetComponent<MeshRenderer>().material = GoldMaterial;
                        break;
                }
            }
        }
        public void HideEmissive()
        {
            if (_inventory.CurrentItemCount == 1)
                Emissive.GetComponent<MeshRenderer>().material = DefaultMaterial;
        }
    }
}
