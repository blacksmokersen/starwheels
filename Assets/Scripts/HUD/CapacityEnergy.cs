using Kart;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class CapacityEnergy : MonoBehaviour
    {
        public Image Energy;

        private void Start()
        {
            KartEvents.Instance.OnEnergyConsumption += UpdateFillAmount;
        }

        public void UpdateFillAmount(float energy)
        {
            Energy.fillAmount = energy;
        }
    }
}
