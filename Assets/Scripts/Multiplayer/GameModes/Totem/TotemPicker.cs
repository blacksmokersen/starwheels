using UnityEngine;
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
        private Throwable _totem;
        private ThrowableLauncher _throwableLauncher;
        private ThrowPositions _throwPositions;

        // CORE

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _throwableLauncher = GetComponent<ThrowableLauncher>();
            _throwPositions = GetComponent<ThrowPositions>();
        }

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer)
            {
                if (other.CompareTag(Constants.Tag.Totem) && other.isTrigger)
                {
                    _totem.transform.SetParent(_throwPositions.BackPosition);
                    _totem.transform.position = _throwPositions.BackPosition.position;

                    TotemLost totemLostEvent = TotemLost.Create();
                    totemLostEvent.NewOwnerID = 1;
                    totemLostEvent.OldOwnerID = 2;
                    totemLostEvent.Send();
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
            if (Input.GetButtonDown(Constants.Input.UseItem) && _totem)
            {
                UseTotem();
            }
        }        

        public void SetTotem(GameObject totem)
        {
            _totem = totem.GetComponentInParent<Throwable>();
            _totem.GetComponent<Rigidbody>().isKinematic = true;
            _totem.GetComponent<SphereCollider>().enabled = false;            

            _inventory.StopAllCoroutines(); // Stop any anti-spam routine
            _inventory.CanUseItem = false;
        }

        public void UnsetTotem()
        {
            _totem.GetComponent<Rigidbody>().isKinematic = false;
            _totem.GetComponent<SphereCollider>().enabled = true;
            _totem.GetComponent<TotemBehaviour>().StartSlowdown();
            _totem.transform.SetParent(null);
            _inventory.CanUseItem = true;

            _totem = null;
        }

        // PRIVATE

        public void UseTotem()
        {            
            _throwableLauncher.Throw(_totem);            
        }
    }
}
