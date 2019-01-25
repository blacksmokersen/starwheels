using Bolt;
using UnityEngine;

namespace Abilities
{
    public class AbilitySelectionInputs : EntityBehaviour, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [SerializeField] private AbilitySetter _abilitySetter;

        private void Update()
        {
            if (entity.isControllerOrOwner)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    Debug.Log(_abilitySetter == null);
                    _abilitySetter.SetAbility(0);
                }
                else if (Input.GetKeyDown(KeyCode.T))
                {
                    Debug.Log("T");
                    _abilitySetter.SetAbility(1);
                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("C");
                    _abilitySetter.SetAbility(2);
                }
            }
        }
    }
}
