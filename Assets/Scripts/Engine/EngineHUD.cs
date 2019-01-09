using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Engine
{
    public class EngineHUD : MonoBehaviour
    {
        [Header("HUD")]
        [SerializeField] private GameObject _speedMeter;
        [SerializeField] private Image _speedBar;
        [SerializeField] private TextMeshProUGUI _speedText;

        // PUBLIC

        public void ObserveKart(GameObject kartRoot)
        {
            var engine = kartRoot.GetComponentInChildren<EngineBehaviour>();
            if (engine)
            {
                engine.OnVelocityChange.AddListener(UpdateSpeedmeter);
            }
        }

        public void UpdateSpeedmeter(float speed)
        {
            _speedBar.fillAmount = speed / 80;
            _speedText.text = "" + speed * 2;
        }

        // PRIVATE

        private void Show()
        {
            _speedMeter.SetActive(true);
        }

        private void Hide()
        {
            _speedMeter.SetActive(false);
        }
    }
}
