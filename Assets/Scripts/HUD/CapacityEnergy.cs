using UnityEngine.UI;

namespace HUD
{
    public class CapacityEnergy : BaseKartComponent
    {
        public Image Energy;

        private void Start()
        {
            kartEvents.OnEnergyConsumption += UpdateFillAmount;
        }

        public void UpdateFillAmount(float energy)
        {
            Energy.fillAmount = energy;
        }
    }
}
