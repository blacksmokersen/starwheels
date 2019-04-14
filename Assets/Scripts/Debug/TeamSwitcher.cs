using UnityEngine;
using Bolt;
using Multiplayer;

namespace SW.DebugUtils
{
    public class TeamSwitcher : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        private PlayerSettings _playerSettings;
        private GameSettings _gameSettings;

        // CORE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
        }

        private void Update()
        {
            if (entity.isAttached && entity.isOwner)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled && Input.GetKeyDown(KeyCode.Alpha8))
            {
                SwitchTeam();
            }
        }

        public void SwitchTeam()
        {
            if (entity.isAttached && entity.isOwner)
            {
                var nextColorSettings = _gameSettings.TeamsListSettings.GetNext(_playerSettings.ColorSettings);
                state.Team = nextColorSettings.TeamEnum.ToString();
                _playerSettings.ColorSettings = nextColorSettings;
            }
        }
    }
}
