using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Inventory))]
    public class ItemDisplayer : MonoBehaviour
    {
        [Header("Shields")]
        public GameObject GreenItem;
        public GameObject PurpleItem;
        public GameObject GoldItem;

        private string _itemNameToDisplay;
        private Inventory _inventory;

        public void Awake()
        {
            _inventory = GetComponent<Inventory>();
        }

        public void DisplayItem()
        {

            _itemNameToDisplay = _inventory.CurrentItem.Name;

            switch(_itemNameToDisplay)
            {
                case "Disk":
                    GreenItem.SetActive(true); //Activating the empty game object green
                    GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                    GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                    GreenItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating the disk on the hierarchie
                    break;

                case "Mine":
                    GreenItem.SetActive(true); //Activating the Green Shield
                    GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                    GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                    GreenItem.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Activating the Mine on the hierarchie
                    break;

                case "Guile":
                    GreenItem.SetActive(true); //Activating the Green Shield
                    GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                    GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                    GreenItem.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true); //Activating the Guile on the hierarchie
                    break;

                case "Rocket":
                    PurpleItem.SetActive(true); //Activating the Purple Shield
                    PurpleItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front purple shield on the hierarchie
                    PurpleItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back purple shield on the hierarchie
                    break;

                case "IonBeam":
                    GoldItem.SetActive(true); //Activating the Purple Shield
                    GoldItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front purple shield on the hierarchie
                    GoldItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back purple shield on the hierarchie
                    break;
            }
        }

        public void HideItem()
        {
            if (_inventory.CurrentItemCount == 1)
            {
                switch (_itemNameToDisplay)
                {
                    case "Disk":
                        GreenItem.SetActive(false); //Activating the empty game object green
                        GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Activating the back green shield on the hierarchie

                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Activating the disk on the hierarchie
                        break;

                    case "Mine":
                        GreenItem.SetActive(false); //Activating the Green Shield
                        GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Activating the back green shield on the hierarchie

                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Activating the Mine on the hierarchie
                        break;

                    case "Guile":
                        GreenItem.SetActive(false); //Activating the Green Shield
                        GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Activating the back green shield on the hierarchie

                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(false); //Activating the Guile on the hierarchie
                        break;

                    case "Rocket":
                        PurpleItem.SetActive(false); //Activating the Purple Shield
                        PurpleItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front purple shield on the hierarchie
                        PurpleItem.transform.GetChild(1).gameObject.SetActive(false); //Activating the back purple shield on the hierarchie
                        break;

                    case "IonBeam":
                        GoldItem.SetActive(false); //Activating the Purple Shield
                        GoldItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front purple shield on the hierarchie
                        GoldItem.transform.GetChild(1).gameObject.SetActive(false); //Activating the back purple shield on the hierarchie
                        break;
                }
            }
        }
    }
}
