using Items;

namespace Abilities
{
    public class NitroAbility : Ability
    {
        public float EnergyConsumptionSpeed;

        // CORE

        // PUBLIC

        public override void Use(float xAxis, float yAxis)
        {
            /*
            if(Energy > 1 / kartInventory.Item.Count && kartInventory.Count > 1)
            {
                Energy = EnergyConsumptionSpeed * Time.deltaTime;
                kartEvents.OnEnergyConsumption(Energy);
            }
            */
        }

        // PRIVATE
    }
}
