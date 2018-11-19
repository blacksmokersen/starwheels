using System.Collections;
using UnityEngine;
using Multiplayer;
using Bolt;
using ThrowingSystem;

namespace GameModes.Totem
{
    [RequireComponent(typeof(TotemPicker))]
    public class TotemPossession : GlobalEventListener
    {
        public BoltEntity TotemEntity;

        private ThrowPositions _throwPositions;
        private TotemPicker _totemPicker;        
        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _throwPositions = GetComponent<ThrowPositions>();
            _totemPicker = GetComponent<TotemPicker>();
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        private void Start()
        {
            StartCoroutine(LookForTotem());
        }

        // BOLT

        public override void OnEvent(TotemThrown evnt)
        {
            if (BoltNetwork.isServer)
            {
                Debug.Log("TotemThrown event sent from " + evnt.OwnerID);

                if (evnt.OwnerID == TotemEntity.GetState<IItemState>().OwnerID)
                {
                    var kartThrowing = MyExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    var totemBehaviour = TotemEntity.GetComponent<TotemBehaviour>();
                    totemBehaviour.SetParent(null);
                    totemBehaviour.SetTotemKinematic(false);
                    kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(TotemEntity.GetComponent<Throwable>());
                    TotemEntity.GetState<IItemState>().OwnerID = -1;
                }
            }
        }

        // PRIVATE

        private IEnumerator LookForTotem()
        {
            while(TotemEntity == null)
            {                
                var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
                if (totem)
                {
                    TotemEntity = totem.GetComponent<BoltEntity>();
                    _totemPicker.TotemEntity = TotemEntity;
                }
                yield return null;
            }
            Debug.Log("Found totem entity !");
        }
    }
}