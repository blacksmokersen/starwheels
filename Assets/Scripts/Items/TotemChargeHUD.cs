using UnityEngine;
using System.Collections;

namespace GameModes.Totem
{
    public class TotemChargeHUD : MonoBehaviour, IObserver
    {
        [Header("HUD Elements")]
        [SerializeField] private GameObject _charging;
        [SerializeField] private GameObject _dicharging;

        // PUBLIC

        public void Observe(GameObject gameObject)
        {
            var totemPossession = gameObject.GetComponentInChildren<TotemPossession>();
            if (totemPossession)
            {
                totemPossession.OnTotemGet.AddListener(ShowDischargingHUD);
                totemPossession.OnTotemLost.AddListener(ShowChargingHUD);
            }
            else
            {
                Debug.LogWarning("TotemPossession component not found.");
            }
        }

        public void ShowChargingHUD()
        {
            StopAllCoroutines();
            StartCoroutine(ShowChargingRoutine());
        }

        public void ShowDischargingHUD()
        {
            StopAllCoroutines();
            //StartCoroutine(ShowDischargingRoutine());
            HideChargingHUD();
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

        private IEnumerator ShowChargingRoutine()
        {
            HideDischargingHUD();
            _charging.SetActive(true);
            yield return new WaitForSeconds(3f);
            _charging.SetActive(false);
        }

        private IEnumerator ShowDischargingRoutine()
        {
            HideChargingHUD();
            _dicharging.SetActive(true);
            yield return new WaitForSeconds(3f);
            _dicharging.SetActive(false);
        }
    }
}
