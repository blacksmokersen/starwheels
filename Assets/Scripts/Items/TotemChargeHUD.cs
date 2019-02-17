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
                totemPossession.OnTotemGet.AddListener(HideChargingHUD);
                totemPossession.OnTotemLost.AddListener(ShowChargingHUD);
                totemPossession.OnTotemLost.AddListener(HideDischargingHUD);
            }
            else
            {
                Debug.Log("TotemPossession component not found.");
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
            StartCoroutine(ShowDischargingRoutine());
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
            _dicharging.SetActive(false);
            _charging.SetActive(true);
            yield return new WaitForSeconds(3);
            _charging.SetActive(false);
        }

        private IEnumerator ShowDischargingRoutine()
        {
            _charging.SetActive(false);
            _dicharging.SetActive(true);
            yield return new WaitForSeconds(3);
            _dicharging.SetActive(false);
        }
    }
}
