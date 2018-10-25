using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

namespace Abilities
{
    public class AbilitiesBehaviour : EntityBehaviour<IKartState>
    {
        public AbilitiesBehaviourSettings abilitiesBehaviourSettings;

        public override void SimulateController()
        {

        }

        /*
        private IEnumerator Cooldown()
        {
            _canUseAbility = false;
            yield return new WaitForSeconds(jumpSettings.CooldownDuration);
            _canUseAbility = true;
            _hasDoneFirstJump = false;
            OnJumpReload.Invoke();
        }
        */
    }
}
