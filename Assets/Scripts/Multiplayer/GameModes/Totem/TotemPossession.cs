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

        private TotemPicker _totemPicker;
        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
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
            }
            else if(evnt.NewOwnerID == _playerSettings.ConnectionID)
            {
                Debug.Log("I am new owner");
                _totemPicker.SetTotem(TotemEntity.gameObject);
                TotemEntity.TakeControl();
            }
        }

        // PRIVATE

        private IEnumerator LookForTotem()
        {
            while(TotemEntity == null)
            {                
                var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
                if (totem) TotemEntity = totem.GetComponent<BoltEntity>();
                yield return null;
            }
            Debug.Log("Found totem entity !");
        }
    }
}