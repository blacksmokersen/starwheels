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

        private void Awake()
        {
            _totemPicker = GetComponent<TotemPicker>();
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        public override void OnEvent(TotemLost evnt)
        {
            if(evnt.OldOwnerID == _playerSettings.ConnectionID)
            {
                _totemPicker.UnsetTotem();
                TotemEntity.ReleaseControl();
            }
            else if(evnt.NewOwnerID == _playerSettings.ConnectionID)
            {
                _totemPicker.SetTotem(TotemEntity.gameObject);
                TotemEntity.TakeControl();
            }
        }
    }
}