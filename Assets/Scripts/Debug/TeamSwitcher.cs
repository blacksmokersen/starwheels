using UnityEngine;
using Bolt;
using Multiplayer.Teams;

namespace KBA.Debug
{
    public class TeamSwitcher : EntityBehaviour<IKartState>, IControllable
    {
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
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SwitchTeam();
            }
        }

        public void SwitchTeam()
        {
            if (entity.isAttached && entity.isOwner)
            {
                UnityEngine.Debug.Log("Switching");
                state.Team = state.Team.GetNextTeamColor();
            }
        }
    }
}
