using UnityEngine;
using UnityEngine.UI;
using Kart;

namespace HUD
{
    public class Speedmeter : MonoBehaviour
    {
        public Image speedBar;
        public Text textSpeed;

        private void Start()
        {
            KartEvents.Instance.OnVelocityChange += SpeedmeterBehaviour;
        }

        public void SpeedmeterBehaviour(Vector3 kartVelocity)
        {
            var speed = Mathf.Round(kartVelocity.magnitude);            
            speedBar.fillAmount = speed / 70;
            SpeedOnScreen(speed);
        }

        private void SpeedOnScreen(float speed)
        {
            var speedString = "" + speed * 2;
            textSpeed.text = speedString.ToString();
        }
    }
}