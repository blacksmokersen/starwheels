using UnityEngine;

namespace Drift
{
    public class DriftEffects : MonoBehaviour
    {

        [SerializeField] private ParticleSystem smokeLeftWheel;
        [SerializeField] private ParticleSystem smokeRightWheel;

        private void Awake()
        {
            StopSmoke();

            // LOGIC
            /*
            kartEvents.OnDriftStart += StartSmoke;
            kartEvents.OnDriftStart += () => SetWheelsColor(Color.white);
            kartEvents.OnDriftEnd += StopSmoke;

            kartEvents.OnDriftOrange += () => SetWheelsColor(Color.yellow);
            kartEvents.OnDriftRed += () => SetWheelsColor(Color.red);
            kartEvents.OnDriftBoostStart += () => SetWheelsColor(Color.green);
            kartEvents.OnDriftBoostEnd += StopSmoke;
            */
        }

        public void StopSmoke()
        {
            //    if (kartStates.DriftState != Kart.DriftState.Turbo)
            //    {
            smokeLeftWheel.Stop(true);
            smokeRightWheel.Stop(true);
            //    }
        }
        public void StartSmoke()
        {
            if (!smokeLeftWheel.isPlaying)
                smokeLeftWheel.Play(true);
            if (!smokeRightWheel.isPlaying)
                smokeRightWheel.Play(true);
        }
        /*
        public void SetWheelsColor(Color color)
        {
            var leftWheelMain = smokeLeftWheel.main;
            var rightWheelMain = smokeRightWheel.main;

            leftWheelMain.startColor = color;
            rightWheelMain.startColor = color;
        }
        */
        public void SetWheelsWhite()
        {
            var leftWheelMain = smokeLeftWheel.main;
            var rightWheelMain = smokeRightWheel.main;

            leftWheelMain.startColor = Color.white;
            rightWheelMain.startColor = Color.white;
        }

        public void SetWheelsOrange()
        {
            var leftWheelMain = smokeLeftWheel.main;
            var rightWheelMain = smokeRightWheel.main;

            leftWheelMain.startColor = Color.yellow;
            rightWheelMain.startColor = Color.yellow;
        }

        public void SetWheelsRed()
        {
            var leftWheelMain = smokeLeftWheel.main;
            var rightWheelMain = smokeRightWheel.main;

            leftWheelMain.startColor = Color.red;
            rightWheelMain.startColor = Color.red;
        }

        public void SetWheelsGreen()
        {
            var leftWheelMain = smokeLeftWheel.main;
            var rightWheelMain = smokeRightWheel.main;

            leftWheelMain.startColor = Color.green;
            rightWheelMain.startColor = Color.green;
        }
    }
}
