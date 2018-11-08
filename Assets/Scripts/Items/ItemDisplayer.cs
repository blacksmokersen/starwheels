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
        ThrowingSystem.ThrowableLauncher _throwableLauncher;
        private Direction _direction;
        

        public void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _throwableLauncher = GetComponent<ThrowingSystem.ThrowableLauncher>();
            
        }

        void Update()
        {
            if(_inventory.CurrentItem != null)
            {
                _itemNameToDisplay = _inventory.CurrentItem.Name;
                _direction = _throwableLauncher.ThrowingDirection;

                switch (_itemNameToDisplay)
                {
                    #region Disk
                    case "Disk":
                        GreenItem.SetActive(true); //Activating the empty game object green
                        GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                        if (_direction == Direction.Default || _direction == Direction.Forward)
                        {
                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating in front the disk 
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Desactivating the disk behind
                        }
                        else if (_direction == Direction.Backward)
                        {
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating the disk behind
                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Desctivating in front the disk 
                        }
                        break;
                    #endregion

                    #region Mine
                    case "Mine":
                        GreenItem.SetActive(true); //Activating the Green Shield
                        GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                        
                        if (_direction == Direction.Backward || _direction == Direction.Default)
                        {
 
                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Desactivating in front the Mine 
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Activating the mine behind

                        }
                        else if (_direction == Direction.Forward)
                        {
                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Activating in front the Mine 
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Desactivating the Mine behind
                        }
                        break;
                    #endregion

                    #region Guile
                    case "Guile":
                        GreenItem.SetActive(true); //Activating the Green Shield
                        GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie



                        if (_direction == Direction.Forward || _direction == Direction.Default)
                        {
                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true); //Activating in front the Guile
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(false); //Desactivating the Guile behind
                        }
                        else if (_direction == Direction.Backward)
                        {
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(true); //Activating the Guile behind
                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(false); //Desactivating in front the Guile
                        }
                        break;

                    #endregion

                    #region Rocket
                    case "Rocket":
                        PurpleItem.SetActive(true); //Activating the Purple Shield
                        PurpleItem.transform.GetChild(0).gameObject.SetActive(true); ;
                        PurpleItem.transform.GetChild(1).gameObject.SetActive(true); ;
                        if (_direction == Direction.Forward || _direction == Direction.Default)
                        {
                            PurpleItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating in front the rocket 
                            PurpleItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Desactivating the rocket behind 
                        }
                        else if (_direction == Direction.Backward)
                        {
                            PurpleItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Desactivating the rocket behind
                            PurpleItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating the rocket in front
                        }


                        break;
                    #endregion

                    case "IonBeam":
                        GoldItem.SetActive(true); //Activating the Purple Shield
                        GoldItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front purple shield on the hierarchie
                        GoldItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back purple shield on the hierarchie
                        break;
                }
            }
                
            
        }
        /*
        public void DisplayItem()
        {
            
            _itemNameToDisplay = _inventory.CurrentItem.Name;
            _direction = _throwableLauncher.ThrowingDirection;

            switch(_itemNameToDisplay)
            {
                #region Disk
                case "Disk":
                    GreenItem.SetActive(true); //Activating the empty game object green
                    GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                    GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                    if (_direction == Direction.Default || _direction == Direction.Forward)
                    {
                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating in front the disk 
                    }
                    else if(_direction == Direction.Backward)
                    {
                        GreenItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating on the back the disk
                    }
                        break;
                #endregion

                #region Mine
                case "Mine":
                    GreenItem.SetActive(true); //Activating the Green Shield
                    GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                    GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                    if (_direction == Direction.Forward)
                    {
                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Activating in front the Mine 
                    }
                    else if (_direction == Direction.Backward || _direction == Direction.Default)
                    {
                        GreenItem.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Activating in front the Mine
                    }
                    break;
                #endregion

                #region Guile
                case "Guile":
                    GreenItem.SetActive(true); //Activating the Green Shield
                    GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                    GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                    

                    if (_direction == Direction.Forward || _direction == Direction.Default)
                    {
                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true); //Activating in front the Guile
                    }
                    else if (_direction == Direction.Backward )
                    {
                        GreenItem.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(true); //Activating in front the Mine
                    }
                    break;

                #endregion

                #region Rocket
                case "Rocket":
                    PurpleItem.SetActive(true); //Activating the Purple Shield
                    PurpleItem.transform.GetChild(0);
                    PurpleItem.transform.GetChild(1);
                    if (_direction == Direction.Forward || _direction == Direction.Default)
                    {
                        PurpleItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front purple shield on the hierarchie
                    }
                    else if (_direction == Direction.Backward)
                    {
                        PurpleItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back purple shield on the hierarchie
                    }
                    
                    
                    break;
                #endregion

                case "IonBeam":
                    GoldItem.SetActive(true); //Activating the Purple Shield
                    GoldItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front purple shield on the hierarchie
                    GoldItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back purple shield on the hierarchie
                    break;
            }
        }
        */

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
