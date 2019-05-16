using UnityEngine;

namespace Abilities.TpBack
{
    public class TpBackAbilitySounds : MonoBehaviour
    {
        public AudioSource Launch;
        public AudioSource Flight;
        public AudioSource Idle;
        public AudioSource Used;

        public void PlayLaunchSound()
        {
            Launch.Play();
        }
        public void PlayFlightSound()
        {
            Flight.Play();
        }
        public void StopFlightSound()
        {
            Flight.Stop();
        }
        public void PlayIdleSound()
        {
            Idle.Play();
        }
        public void StopIdleSound()
        {
            Idle.Stop();
        }
        public void PlayUsedSound()
        {
            Used.Play();
        }
    }
}
