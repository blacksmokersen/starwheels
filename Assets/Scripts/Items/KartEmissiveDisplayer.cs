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
        private Inventory _inventory;

        public void Awake()
        {
            _inventory = GetComponent<Inventory>();
            Emissive.GetComponent<MeshRenderer>();
        }

        public override void OnEvent(ShowKartDisplayItem evnt)
        {
            var entity = GetComponentInParent<BoltEntity>();

            if (entity == evnt.Entity)
            {
                _emissiveToDisplay = evnt.ItemName;

                if (evnt.ItemCount > 0)
                {
                    DisplayEmissive();
                }
                else
                {
                    HideEmissive();
                }
            }
        }

        public void DisplayEmissive()
        {
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

        public void HideEmissive()
        {
            Emissive.GetComponent<MeshRenderer>().material = DefaultMaterial;
        }
    }
}
