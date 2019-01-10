using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Engine
{
    public class EngineHUD : MonoBehaviour
    {
        [Header("HUD")]
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
            _speedBar.fillAmount = speed / 40;
            _speedText.text = "" + (int) speed * 2;
        }

        // PRIVATE

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
