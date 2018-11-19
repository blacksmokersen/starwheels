using System.Collections;
using UnityEngine;
using Bolt;
using ThrowingSystem;
using Items;
using Multiplayer;

namespace GameModes.Totem
{
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(ThrowableLauncher))]
    [RequireComponent(typeof(ThrowPositions))]
    public class TotemPicker : EntityBehaviour<IKartState> , IControllable
    {
        public BoltEntity TotemEntity;

        private Inventory _inventory;
        private ThrowPositions _throwPositions;
        private PlayerSettings _playerSettings;
        private bool _canPickup = true;

        // CORE

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _throwPositions = GetComponent<ThrowPositions>();
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(Constants.Tag.Totem) && other.isTrigger && _canPickup)
            {
                if (BoltNetwork.isServer)
                {
                    Debug.Log("Someone got the totem !");
                    other.GetComponentInParent<BoltEntity>().GetState<IItemState>().OwnerID = state.OwnerID;
                    TotemEntity.GetComponent<TotemBehaviour>().SetTotemKinematic(true);
                    TotemEntity.GetComponent<TotemBehaviour>().SetParent(_throwPositions.BackPosition);
                    StartCoroutine(AntiPickSpamRoutine());
                }
            }
        }


        // BOLT

        public override void SimulateController()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseItem))
            {
                UseTotem();
            }
        }

        public void SetTotem(GameObject totem)
        {
            _inventory.StopAllCoroutines(); // Stop any anti-spam routine
            _inventory.CanUseItem = false;
        }

        public void UnsetTotem()
        {            
            _inventory.CanUseItem = true;
        }

        // PRIVATE

        private void UseTotem()
        {
            TotemThrown totemThrownEvent = TotemThrown.Create();
            totemThrownEvent.OwnerID = state.OwnerID;
            totemThrownEvent.Send();
        }

        private IEnumerator AntiPickSpamRoutine()
        {
            _canPickup = false;
            yield return new WaitForSeconds(1f);
            _canPickup = true;
        }
    }
}
