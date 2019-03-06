using UnityEngine;
using SWExtensions;

namespace Items
{
    public class OverchargeSounds : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _activationAudioSource;
        [SerializeField] private AudioSource _deactivationAudioSource;

        public void PlayActivationSound()
        {
            _activationAudioSource.Play();
        }

        public void PlayDeactivationSound()
        {
            AudioExtensions.PlayClipObjectAndDestroy(_deactivationAudioSource);
        }
    }
}
