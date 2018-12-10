using UnityEngine;
using Bolt;
using UnityEngine.Events;
using System;

namespace Items
{
    [RequireComponent(typeof(Inventory))]
    public class ItemDisplayer : EntityBehaviour<IKartState>
    {
        [Header("Shields")]
        public GameObject GreenItem;
        public GameObject PurpleItem;
        public GameObject GoldItem;

        [Header("Events")]
        public UnityEvent OnJoystickDirectionChanged;

        private Inventory _inventory;
        private ThrowingSystem.ThrowableLauncher _throwableLauncher;
        private Direction _direction;
        private bool _itemIsForward = false;
        private bool _itemIsBackward = false;

        // CORE

        public void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _throwableLauncher = GetComponent<ThrowingSystem.ThrowableLauncher>();
        }

        // BOLT

        public override void SimulateController()
        {
            CheckAxis();
        }

        // PUBLIC

        public void SendShowDisplayEvent()
        {
            if (_inventory.CurrentItem != null)
            {
                Debug.Log("Sending event for item : " + _inventory.CurrentItem.Name);
                var showDisplayEvent = ShowKartDisplayItem.Create();
                showDisplayEvent.Entity = GetComponentInParent<BoltEntity>();
                showDisplayEvent.ItemName = _inventory.CurrentItem.Name;
                showDisplayEvent.ItemCount = _inventory.CurrentItemCount;
                showDisplayEvent.Direction = _throwableLauncher.GetThrowingDirection().ToString();
                showDisplayEvent.Send();
            }
        }

        public void DisplayItem(string itemNameToDisplay, int itemCountToDisplay, Direction direction)
        {
            HideItem();

            _direction = direction;

            if (itemCountToDisplay > 0)
            {
                switch (itemNameToDisplay)
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

                    #region IonBeam
                    case "IonBeam":
                        GoldItem.SetActive(true); //Activating the Purple Shield
                        GoldItem.transform.GetChild(0).gameObject.SetActive(true); //Activating the front purple shield on the hierarchie
                        GoldItem.transform.GetChild(1).gameObject.SetActive(true); //Activating the back purple shield on the hierarchie
                        break;
                        #endregion
                }
            }
        }

        public void HideItem()
        {
            GreenItem.SetActive(false); //Activating the empty game object green
            PurpleItem.SetActive(false); //Activating the Purple Shield
            GoldItem.SetActive(false); //Activating the Purple Shield
        }

        // PRIVATE

        private void CheckAxis()
        {
            if (Input.GetAxis(Constants.Input.UpAndDownAxis) > 0.3f && _itemIsForward == false)
            {
                _itemIsForward = true;
                _itemIsBackward = false;
                SendShowDisplayEvent();

            }
            else if (Input.GetAxis(Constants.Input.UpAndDownAxis) < -0.3f && _itemIsBackward == false)
            {
                _itemIsBackward = true;
                _itemIsForward = false;
                SendShowDisplayEvent();
            }
            else if(Math.Abs(Input.GetAxis(Constants.Input.UpAndDownAxis)) < 0.3f && (_itemIsBackward || _itemIsForward))
            {
                _itemIsForward = false;
                _itemIsBackward = false;
                SendShowDisplayEvent();
            }
        }
    }
}
