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
        private Throwable _totem;
        private ThrowableLauncher _throwableLauncher;
        private ThrowPositions _throwPositions;
        private bool _thrown = false;
        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _throwableLauncher = GetComponent<ThrowableLauncher>();
            _throwPositions = GetComponent<ThrowPositions>();
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(Constants.Tag.Totem) && other.isTrigger)
            {
                if (BoltNetwork.isServer)
                {
                    Debug.Log("Someone got the totem !");
                    //_totem = other.GetComponentInParent<Throwable>();

                    var totemState = other.GetComponentInParent<BoltEntity>().GetState<IItemState>();

                    TotemLost totemLostEvent = TotemLost.Create();
                    totemLostEvent.NewOwnerID = state.OwnerID;
                    totemLostEvent.OldOwnerID = totemState.OwnerID;
                    totemLostEvent.Send();

                    totemState.OwnerID = state.OwnerID;
                }
            }
        }

        // BOLT

        public override void SimulateController()
        {
            MapInputs();       
            if(TotemEntity && _totem)// TotemEntity.GetState<IItemState>().OwnerID == _playerSettings.ConnectionID)
            {
                TotemEntity.transform.position = _throwPositions.BackPosition.position;
            }
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
            Debug.Log("Totem GO locally set.");
            _totem = totem.GetComponentInParent<Throwable>();            

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
            UnsetTotem();
            _totem.GetComponent<TotemBehaviour>().SetTotemKinematic(false);
            _totem.GetComponent<TotemBehaviour>().StartSlowdown();
            _throwableLauncher.Throw(_totem);           
            _totem = null;
        }
    }
}
