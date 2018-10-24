using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

namespace Abilities
{
    public class AbilitiesBehaviour : EntityBehaviour<IKartState>, IControllable
    {
        public AbilitiesBehaviourSettings abilitiesBehaviourSettings;

        private void Awake()
        {
            abilitiesBehaviourSettings = Resources.Load<AbilitiesBehaviourSettings>("AbilitiesBehaviourSettings");
        }


        public override void SimulateController()
        {

        }

        public void MapInputs()
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
