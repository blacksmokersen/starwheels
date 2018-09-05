using Items;
using UnityEngine;

namespace Abilities
{
    public class TPBackAbility : Ability
    {
        private TPBackBehaviour _tpBack = null;

        // CORE

        // PUBLIC

        public override void Use(float xAxis, float yAxis)
        {
            if (_tpBack == null)
            {
                Direction direction = Direction.Backward;
                if (yAxis > 0) direction = Direction.Forward;

                _tpBack = ((GameObject)Instantiate(Resources.Load("TPBack"))).GetComponent<TPBackBehaviour>();
                _tpBack.Launch(kartHub.kartInventory, direction);
            }
            else //if (_tpBack.IsEnabled())
            {
                kartHub.transform.position = _tpBack.transform.position;
                Destroy(_tpBack.gameObject);
            }
        }

        // PRIVATE
    }
}
