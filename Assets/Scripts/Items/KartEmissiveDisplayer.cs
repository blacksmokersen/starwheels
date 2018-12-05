using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Items
{

    [RequireComponent(typeof(Inventory))]
    public class KartEmissiveDisplayer : GlobalEventListener
    {

        [Header("Materials Emissives")]
        public Material GreenMaterial;
        public Material PurpleMaterial;
        public Material GoldMaterial;
        public Material DefaultMaterial;

        public GameObject Emissive;

        private string _emissiveToDisplay;
        private int _itemCountToDisplay;
        //private Inventory _inventory;


        public void Awake()
        {
            //_inventory = GetComponent<Inventory>();
            Emissive.GetComponent<MeshRenderer>();

        }




        public override void OnEvent(HideKartDisplayItem hideKartDisplayItem)
        {
            var entity = GetComponentInParent<BoltEntity>();

            if (entity == hideKartDisplayItem.Entity)
            {
                _emissiveToDisplay = hideKartDisplayItem.ItemName;
                _itemCountToDisplay = hideKartDisplayItem.ItemCount;
                HideEmissive();
            }
        }

        public override void OnEvent(ShowKartDisplayItem showKartDisplayItem)
        {
            var entity = GetComponentInParent<BoltEntity>();

            if (entity == showKartDisplayItem.Entity)
            {
                _emissiveToDisplay = showKartDisplayItem.ItemName;
                _itemCountToDisplay = showKartDisplayItem.ItemCount;
                DisplayEmissive();
            }
        }

        public void DisplayEmissive()
        {
       //     if (_inventory.CurrentItem != null)
       //     {
              //  _emissiveToDisplay = _inventory.CurrentItem.Name;

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
       //     }
        }

        public void HideEmissive()
        {
            if (_itemCountToDisplay == 1)
                Emissive.GetComponent<MeshRenderer>().material = DefaultMaterial;
        }
    }
}
