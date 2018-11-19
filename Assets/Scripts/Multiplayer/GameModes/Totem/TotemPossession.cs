using System.Collections;
using UnityEngine;
using Multiplayer;
using Bolt;

namespace GameModes.Totem
{
    [RequireComponent(typeof(TotemPicker))]
    public class TotemPossession : GlobalEventListener
    {
        public BoltEntity TotemEntity;

        private ThrowingSystem.ThrowPositions _throwPositions;
        private TotemPicker _totemPicker;        
        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _throwPositions = GetComponent<ThrowingSystem.ThrowPositions>();
            _totemPicker = GetComponent<TotemPicker>();
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        private void Start()
        {
            StartCoroutine(LookForTotem());
        }

        // BOLT

        public override void OnEvent(TotemLost evnt)
        {
            if(evnt.OldOwnerID == _playerSettings.ConnectionID)
            {
                Debug.Log("I was old owner");
                _totemPicker.UnsetTotem();
                TotemEntity.ReleaseControl();
                TotemEntity.transform.SetParent(null);                
            }
            else if(evnt.NewOwnerID == _playerSettings.ConnectionID)
            {
                Debug.Log("I am new owner");
                _totemPicker.SetTotem(TotemEntity.gameObject);
                Debug.Log("Entity is controlled : " + TotemEntity.isControlled);
                TotemEntity.TakeControl();
                TotemEntity.transform.SetParent(_throwPositions.BackPosition);
                TotemEntity.transform.localPosition = Vector3.zero;
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