using UnityEngine;
using Bolt;
using UnityEngine.Events;
using System;

namespace Items
{
    [RequireComponent(typeof(Inventory))]
    public class ItemDisplayer : GlobalEventListener
    {
        [Header("Shields")]
        public GameObject GreenItem;
        public GameObject PurpleItem;
        public GameObject GoldItem;

        private string _itemNameToDisplay;
        private int _itemCountToDisplay;
        private Inventory _inventory;
        private ThrowingSystem.ThrowableLauncher _throwableLauncher;
        private Direction _direction;

        public UnityEvent CheckDirection;

        public void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _throwableLauncher = GetComponent<ThrowingSystem.ThrowableLauncher>();
        }


        public void Update()
        {
            CheckAxis();
        }

        

        public void HideDisplayEvent()
        {
            var hideDisplayEvent = HideKartDisplayItem.Create();
            hideDisplayEvent.Entity = GetComponentInParent<BoltEntity>();
            if (_inventory.CurrentItem != null)
            {
                hideDisplayEvent.ItemName = _inventory.CurrentItem.Name;
                hideDisplayEvent.ItemCount = _inventory.CurrentItemCount;
                hideDisplayEvent.Send();
            }
        }

        public override void OnEvent(HideKartDisplayItem hideKartDisplayItem)
        {
            var entity = GetComponentInParent<BoltEntity>();

            if (entity == hideKartDisplayItem.Entity)
            {
                _itemNameToDisplay = hideKartDisplayItem.ItemName;
                _itemCountToDisplay = hideKartDisplayItem.ItemCount;
                HideItem();
            }
        }

        public void ShowDisplayEvent()
        {
            var showDisplayEvent = ShowKartDisplayItem.Create();
            showDisplayEvent.Entity = GetComponentInParent<BoltEntity>();
            if (_inventory.CurrentItem != null)
            {
                showDisplayEvent.ItemName = _inventory.CurrentItem.Name;
                showDisplayEvent.ItemCount = _inventory.CurrentItemCount;
                //  showDisplayEvent.Direction = _throwableLauncher.ThrowingDirection.ToString();
                showDisplayEvent.Send();
            }
        }

        public override void OnEvent(ShowKartDisplayItem showKartDisplayItem)
        {
            var entity = GetComponentInParent<BoltEntity>();

            if (entity == showKartDisplayItem.Entity)
            {
                _itemNameToDisplay = showKartDisplayItem.ItemName;
                _itemCountToDisplay = showKartDisplayItem.ItemCount;
                _direction = Direction.Forward;
                
                DisplayItem();
            }
        }

        public void DisplayItem()
        {
            if (_inventory.CurrentItemCount > 0)
            {
                //  _itemNameToDisplay = _inventory.CurrentItem.Name;
                _direction = _throwableLauncher.GetThrowingDirection();



                switch (_itemNameToDisplay)
                {
                    #region Disk
                    case "Disk":
                        GreenItem.SetActive(true); //Activating the empty game object green
                        if (_direction == Direction.Default || _direction == Direction.Forward)
                        {
                            GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                            GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Desactivating the back green shield on the hierarchie

                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating in front the disk
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Desactivating the disk behind
                        }
                        else if (_direction == Direction.Backward)
                        {
                            GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Desactivating the front green shield on the hierarchie
                            GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating the disk behind
                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Desctivating in front the disk

                        }
                        break;
                    #endregion

                    #region Mine
                    case "Mine":
                        GreenItem.SetActive(true); //Activating the Green Shield
                        if (_direction == Direction.Backward || _direction == Direction.Default)
                        {

                            GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front green shield on the hierarchie
                            GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Desactivating the back green shield on the hierarchie

                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Desactivating in front the Mine
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Activating the mine behind

                        }

                        else if (_direction == Direction.Forward)
                        {

                            GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                            GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Desactivating the back green shield on the hierarchie

                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true); //Activating in front the Mine
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Desactivating the Mine behind
                        }


                        break;
                    #endregion

                    #region Guile
                    case "Guile":
                        GreenItem.SetActive(true); //Activating the Green Shield
                        if (_direction == Direction.Forward || _direction == Direction.Default)
                        {
                            GreenItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front green shield on the hierarchie
                            GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Desactivating the back green shield on the hierarchie

                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true); //Activating in front the Guile
                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(false); //Desactivating the Guile behind

                        }


                        else if (_direction == Direction.Backward)
                        {
                            GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Desactivating the front green shield on the hierarchie
                            GreenItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back green shield on the hierarchie

                            GreenItem.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(true); //Activating the Guile behind
                            GreenItem.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(false); //Desactivating in front the Guile
                        }

                        break;

                    #endregion

                    #region Rocket
                    case "Rocket":
                        PurpleItem.SetActive(true); //Activating the Purple Shield



                        if (_direction == Direction.Forward || _direction == Direction.Default)
                        {

                            PurpleItem.transform.GetChild(0).gameObject.SetActive(true);
                            PurpleItem.transform.GetChild(1).gameObject.SetActive(false);

                            PurpleItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true); //Activating in front the rocket
                            PurpleItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Desactivating the rocket behind
                        }
                        else if (_direction == Direction.Backward)
                        {

                            PurpleItem.transform.GetChild(0).gameObject.SetActive(false);
                            PurpleItem.transform.GetChild(1).gameObject.SetActive(true);


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
            else
            {
                HideItem();
            }
        }

        public void HideItem()
        {
            if (_itemCountToDisplay == 1)
            {
                switch (_itemNameToDisplay)
                {
                    case "Disk":
                        GreenItem.SetActive(false); //Activating the empty game object green
                        GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Activating the back green shield on the hierarchie

                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Activating the disk on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false); //Activating the Mine on the hierarchie
                        break;

                    case "Mine":
                        GreenItem.SetActive(false); //Activating the Green Shield
                        GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Activating the back green shield on the hierarchie

                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Activating the Mine on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false); //Activating the Mine on the hierarchie
                        break;

                    case "Guile":
                        GreenItem.SetActive(false); //Activating the Green Shield
                        GreenItem.transform.GetChild(0).gameObject.SetActive(false); //Activating the front green shield on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.SetActive(false); //Activating the back green shield on the hierarchie

                        GreenItem.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(false); //Activating the Guile on the hierarchie
                        GreenItem.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.SetActive(false); //Activating the Mine on the hierarchie
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

        private void CheckAxis()
        {
            if(Mathf.Abs(Input.GetAxis(Constants.Input.UpAndDownAxis)) != 0.3)
            {
            CheckDirection.Invoke();

        }
        }
    }
}
