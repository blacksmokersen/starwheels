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
        private BoltEntity _totemEntity;

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

            if (other.CompareTag(Constants.Tag.Totem) && other.isTrigger)
            {
                if (BoltNetwork.isServer)
                {
                    var totem = other.GetComponentInParent<Throwable>();
                    
                    totem.GetComponent<Rigidbody>().isKinematic = true;
                    totem.GetComponent<SphereCollider>().enabled = false;

                    TotemLost totemLostEvent = TotemLost.Create();
                    totemLostEvent.NewOwnerID = state.OwnerID;
                    totemLostEvent.OldOwnerID = other.GetComponentInParent<BoltEntity>().GetState<IItemState>().OwnerID;
                    Debug.Log("Old owner ID = " + totemLostEvent.OldOwnerID);
                    totemLostEvent.Send();
                }
            }
        }

        // BOLT

        public override void SimulateController()
        {
            MapInputs();
            if(_totem) _totem.transform.position = _throwPositions.BackPosition.position;
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
            _totemEntity = _totem.GetComponent<BoltEntity>();

            _inventory.StopAllCoroutines(); // Stop any anti-spam routine
            _inventory.CanUseItem = false;
        }

        public void UnsetTotem()
        {
            _totem.GetComponent<Rigidbody>().isKinematic = false;
            _totem.GetComponent<SphereCollider>().enabled = true;                        

            _inventory.CanUseItem = true;
        }

        // PRIVATE

        public void UseTotem()
        {
            UnsetTotem();
            _throwableLauncher.Throw(_totem);
            _totem.GetComponent<TotemBehaviour>().StartSlowdown();
            _totem = null;
        }
    }
}
