using UnityEngine;
using Bolt;

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

        // CORE

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
                //state.Team = state.Team.GetNextTeamColor();
            }
        }
    }
}
