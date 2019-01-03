using UnityEngine;

namespace Knockout
{
    public class KnockoutSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _knockoutAudioSource;

        public void PlayKnockoutSound()
        {
            _knockoutAudioSource.Play();
        }
    }
}
