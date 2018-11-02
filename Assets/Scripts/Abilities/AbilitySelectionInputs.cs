using Bolt;
using UnityEngine;

namespace Abilities
{
    public class AbilitySelectionInputs : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private AbilitiesBehaviourSettings abilitiesBehaviourSettings;

        private int _abilityNumber = 0;

        // BOLT

        public override void SimulateController()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                _abilityNumber = 0;
                ChangeAbility();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                _abilityNumber = 1;
                ChangeAbility();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _abilityNumber = 2;
                ChangeAbility();
            }
        }

        // PRIVATE

        private void ChangeAbility()
        {
            switch (_abilityNumber)
            {
                case 0:
                    abilitiesBehaviourSettings.ActiveAbility = "Jump";
                    break;
                case 1:
                    abilitiesBehaviourSettings.ActiveAbility = "Hook";
                    break;
                case 2:
                    abilitiesBehaviourSettings.ActiveAbility = "TPBack";
                    break;
            }
        }
    }
}
