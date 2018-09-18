using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class VoiceLaneManager : BaseKartComponent
    {
        [Header("Audio")]
        [SerializeField] private AudioSource voiceLaneSource;
        [SerializeField] private List<AudioClip> hitAudioClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> hurtAudioClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> selectedAudioClips = new List<AudioClip>();

        [Header("Randomization")]
        [SerializeField] private int rememberTheLastXClips = 2;

        private Queue<AudioClip> _lastPlayedHitClips = new Queue<AudioClip>();
        private Queue<AudioClip> _lastPlayedHurtClips = new Queue<AudioClip>();
        private Queue<AudioClip> _lastPlayedSelectedClips = new Queue<AudioClip>();

        // CORE

        private new void Awake()
        {
            base.Awake();

            PlayRandomSelectedVoice();

            kartEvents.OnHitSomeoneElse += PlayRandomHitVoice;
            kartEvents.OnHit += PlayRandomHurtVoice;
        }

        // PUBLIC

        // PRIVATE

        private void PlayRandomHitVoice()
        {
            if (_lastPlayedHitClips.Count > rememberTheLastXClips)
            {
                hitAudioClips.Add(_lastPlayedHitClips.Dequeue());
            }
            var clipToPlay = hitAudioClips[Random.Range(0, hitAudioClips.Count)];
            hitAudioClips.Remove(clipToPlay);
            voiceLaneSource.clip = clipToPlay;
            _lastPlayedHitClips.Enqueue(clipToPlay);
            voiceLaneSource.Play();
        }

        private void PlayRandomHurtVoice()
        {
            if (_lastPlayedHurtClips.Count > rememberTheLastXClips)
            {
                hurtAudioClips.Add(_lastPlayedHurtClips.Dequeue());
            }
            var clipToPlay = hurtAudioClips[Random.Range(0, hurtAudioClips.Count)];
            hurtAudioClips.Remove(clipToPlay);
            voiceLaneSource.clip = clipToPlay;
            _lastPlayedHurtClips.Enqueue(clipToPlay);
            voiceLaneSource.Play();
        }

        private void PlayRandomSelectedVoice()
        {
            if (_lastPlayedSelectedClips.Count > rememberTheLastXClips)
            {
                selectedAudioClips.Add(_lastPlayedSelectedClips.Dequeue());
            }
            var clipToPlay = selectedAudioClips[Random.Range(0, selectedAudioClips.Count)];
            selectedAudioClips.Remove(clipToPlay);
            voiceLaneSource.clip = clipToPlay;
            _lastPlayedSelectedClips.Enqueue(clipToPlay);
            voiceLaneSource.Play();
        }
    }
}
