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

        [SerializeField] private AbilitySelectionPanel _abilityPanel;

        // CORE

        private void Update()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightAlt) && Input.GetKeyDown(KeyCode.Equals))
            {
                Enabled = !Enabled;
            }

            if (Enabled)
            {
                if (Input.GetKeyDown(KeyCode.A))// || Input.GetButtonDown("joystick button 6"))
                {
                    _abilityPanel.ShowPanel();
                }
            }
        }
    }
}
