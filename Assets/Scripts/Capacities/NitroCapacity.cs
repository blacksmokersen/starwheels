using Items;

namespace Capacities
{
    public class NitroCapacity : Capacity
    {
        public float EnergyConsumptionSpeed;

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
    }
}