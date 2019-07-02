using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Engine
{
    public class EngineHUD : MonoBehaviour, IObserver
    {
        [Header("HUD")]
        [SerializeField] private Image _speedBar;
        [SerializeField] private TextMeshProUGUI _speedText;

        // PUBLIC

        public void Observe(GameObject kartRoot)
        {
            var engine = kartRoot.GetComponentInChildren<EngineBehaviour>();
            if (engine)
            {
                engine.OnVelocityChange.AddListener(UpdateSpeedmeter);
            }
        }

        public void UpdateSpeedmeter(float speed)
        {
            speed = Mathf.Abs(speed);
            _speedBar.fillAmount = speed / 35;
            _speedText.text = "" + (int) speed * 4;
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
