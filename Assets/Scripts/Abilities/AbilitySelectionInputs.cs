using Bolt;
using UnityEngine;

namespace Abilities
{
    public class AbilitySelectionInputs : EntityBehaviour, IControllable
    {
        [SerializeField] private AbilitySetter _abilitySetter;

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
                Debug.Log(_abilitySetter == null);
                _abilitySetter.SetJumpingAbility();
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("T");
                _abilitySetter.SetTPBackAbility();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("C");
                _abilitySetter.SetCloakAbility();
            }
        }
    }
}
