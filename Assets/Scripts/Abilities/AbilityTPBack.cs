using Items;
using UnityEngine;

namespace Abilities
{
    public class AbilityTPBack : MonoBehaviour, IControllable
    {
        private TPBackBehaviour _tpBack = null;
        private bool _canUseAbility = true;

        // CORE

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {

            }
        }

        public void Use(float xAxis, float yAxis)
        {
            if (_canUseAbility)
            {
                if (_tpBack == null)
                {
                    Direction direction = Direction.Forward;
                    if (yAxis < 0) direction = Direction.Backward;

                    _tpBack = ((GameObject)Instantiate(Resources.Load("Items/TPBack"))).GetComponent<TPBackBehaviour>();
                    //_tpBack.Launch(kartHub.kartInventory, direction);
                }
                else //if (_tpBack.IsEnabled())
                {
                    //kartHub.transform.position = _tpBack.transform.position;
                    //kartHub.transform.rotation = _tpBack.GetKartRotation();
                    Destroy(_tpBack.gameObject);
                    //StartCoroutine(AbilityCooldown());
                }
            }
        }

        // PRIVATE
    }
}
