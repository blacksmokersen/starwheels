using UnityEngine;
using System.Collections;

namespace Gamemodes.Totem
{
    public class TotemChargeHUD : MonoBehaviour, IObserver
    {
        [Header("HUD Elements")]
        [SerializeField] private GameObject _charging;
        [SerializeField] private GameObject _dicharging;

        // PUBLIC

        public void Observe(GameObject gameObject)
        {
            var totemCharger = gameObject.GetComponentInChildren<KartEnergyDischarger>();
            if (totemCharger)
            {
                totemCharger.OnFullyDischarged.AddListener(() => { HideChargingHUD(); ShowDischargingHUD(); });
                totemCharger.OnStartedCharging.AddListener(() => { ShowChargingHUD(); HideDischargingHUD(); });
                totemCharger.OnFullyCharged.AddListener(() => { HideChargingHUD(); HideDischargingHUD(); });
            }
            else
            {
                Debug.LogWarning("TotemEvents component not found.");
            }
        }

        public void ShowChargingHUDForXSeconds(float x)
        {
            StopAllCoroutines();
            StartCoroutine(ShowChargingHUDForXSecondsRoutine(x));
        }

        public void ShowDischargingHUDForXSeconds(float x)
        {
            StopAllCoroutines();
            StartCoroutine(ShowDischargingHUDForXSecondsRoutine(x));
        }

        public void ShowChargingHUD()
        {
            _charging.SetActive(true);
        }

        public void ShowDischargingHUD()
        {
            _dicharging.SetActive(true);
        }

        public void HideChargingHUD()
        {
            _charging.SetActive(false);
        }

        public void HideDischargingHUD()
        {
            _dicharging.SetActive(false);
        }

        // PRIVATE

        private IEnumerator ShowChargingHUDForXSecondsRoutine(float x)
        {
            HideDischargingHUD();
            ShowChargingHUD();
            yield return new WaitForSeconds(x);
            HideChargingHUD();
        }

        private IEnumerator ShowDischargingHUDForXSecondsRoutine(float x)
        {
            HideChargingHUD();
            ShowDischargingHUD();
            yield return new WaitForSeconds(x);
            HideDischargingHUD();
        }
    }
}
