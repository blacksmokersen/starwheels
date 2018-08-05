using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class Speedmeter : BaseKartComponent
    {
        public Image speedBar;
        public Text textSpeed;

        private void Start()
        {
            kartEvents.OnVelocityChange += SpeedmeterBehaviour;
        }

        public void SpeedmeterBehaviour(float kartVelocity)
        {
            var speed = Mathf.Round(kartVelocity);            
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