﻿using UnityEngine;
using Bolt;
using ThrowingSystem;
using Items;

namespace GameModes.Totem
{
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(ThrowableLauncher))]
    [RequireComponent(typeof(ThrowPositions))]
    public class TotemPicker : EntityBehaviour<IKartState> , IControllable
    {
        private Inventory _inventory;
        private ThrowPositions _throwPositions;

        // CORE

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _throwPositions = GetComponent<ThrowPositions>();
        }

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {

            if (BoltNetwork.isServer && other.CompareTag(Constants.Tag.Totem) && other.isTrigger) // Server sees a player collide with totem trigger
            {
                var totemBehaviour = other.GetComponent<TotemBehaviour>();
                if (totemBehaviour.CanBePickedUp)
                {
                    other.GetComponentInParent<BoltEntity>().GetState<IItemState>().OwnerID = state.OwnerID;
                    totemBehaviour.SetTotemKinematic(true);
                    totemBehaviour.SetParent(_throwPositions.BackPosition);
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

        // PRIVATE

        private void UseTotem()
        {
            TotemThrown totemThrownEvent = TotemThrown.Create();
            totemThrownEvent.OwnerID = state.OwnerID;
            totemThrownEvent.Send();
        }
    }
}
