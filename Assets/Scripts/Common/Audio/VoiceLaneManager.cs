using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Multiplayer;


namespace Audio
{
    public class VoiceLaneManager : GlobalEventListener
    {
        [Header("Entity")]
        [SerializeField] private BoltEntity _entity;

        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;

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


        [Header("Leonie")]
        [SerializeField] private List<AudioClip> leonieHitAudioClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> leonieHurtAudioClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> leonieSelectedAudioClips = new List<AudioClip>();

        [Header("Aconit")]
        [SerializeField] private List<AudioClip> aconitHitAudioClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> aconitHurtAudioClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> aconitSelectedAudioClips = new List<AudioClip>();

        [Header("Will")]
        [SerializeField] private List<AudioClip> willHitAudioClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> willHurtAudioClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> willSelectedAudioClips = new List<AudioClip>();



        private List<AudioClip>[] _currentCharacterVoicelines;

        private List<AudioClip> CurrentHitVoicelines;
        private List<AudioClip> CurrentHurtVoicelines;
        private List<AudioClip> CurrentSelectedVoicelines;

        // CORE

        private void Awake()
        {

            SetVoicelinesWithLocalSettings();

        }

        private void Start()
        {
            if (_entity && _entity.IsOwner)
            {
                SetVoicelinesWithLocalSettings();
            }
        }

        //BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            if (!evnt.Entity.IsOwner && evnt.Entity == _entity)
            {
                SetVoicelines(evnt.CharacterIndex);
            }
        }

        public override void OnEvent(PlayerInfoEvent evnt)
        {
            if (evnt.TargetPlayerID == SWMatchmaking.GetMyBoltId() && // This event is for me
                evnt.KartEntity == _entity && // This is the targetted kart
                !evnt.KartEntity.IsOwner) // I don't own this kart
            {
                SetVoicelines(evnt.CharacterIndex);
            }
        }

        // PUBLIC


        public void SetVoicelinesWithLocalSettings()
        {
            SetVoicelines(_playerSettings.CharacterIndex);
        }

        public void SetVoicelines(int index)
        {
            if (_playerSettings.CharacterIndex == 0)
                _currentCharacterVoicelines = new List<AudioClip>[3] { leonieHitAudioClips, leonieHurtAudioClips, leonieSelectedAudioClips };
            else if (_playerSettings.CharacterIndex == 1)
                _currentCharacterVoicelines = new List<AudioClip>[3] { aconitHitAudioClips, aconitHurtAudioClips, aconitSelectedAudioClips };
            else if (_playerSettings.CharacterIndex == 2)
                _currentCharacterVoicelines = new List<AudioClip>[3] { willHitAudioClips, willHurtAudioClips, willSelectedAudioClips };

            CurrentHitVoicelines = _currentCharacterVoicelines[0];
            CurrentHurtVoicelines = _currentCharacterVoicelines[1];
            CurrentSelectedVoicelines = _currentCharacterVoicelines[2];

            PlayRandomHitVoice();

        }

        public void PlayRandomHitVoice()
        {
            if (_lastPlayedHitClips.Count > rememberTheLastXClips)
            {
                CurrentHitVoicelines.Add(_lastPlayedHitClips.Dequeue());
            }
            var clipToPlay = CurrentHitVoicelines[Random.Range(0, CurrentHitVoicelines.Count)];
            CurrentHitVoicelines.Remove(clipToPlay);
            voiceLaneSource.clip = clipToPlay;
            _lastPlayedHitClips.Enqueue(clipToPlay);
            voiceLaneSource.Play();
        }

        public void PlayRandomHurtVoice()
        {
            if (_lastPlayedHurtClips.Count > rememberTheLastXClips)
            {
                CurrentHurtVoicelines.Add(_lastPlayedHurtClips.Dequeue());
            }
            var clipToPlay = CurrentHurtVoicelines[Random.Range(0, CurrentHurtVoicelines.Count)];
            CurrentHurtVoicelines.Remove(clipToPlay);
            voiceLaneSource.clip = clipToPlay;
            _lastPlayedHurtClips.Enqueue(clipToPlay);
            voiceLaneSource.Play();
        }

        public void PlayRandomSelectedVoice()
        {
            if (_lastPlayedSelectedClips.Count > rememberTheLastXClips)
            {
                CurrentSelectedVoicelines.Add(_lastPlayedSelectedClips.Dequeue());
            }
            var clipToPlay = CurrentSelectedVoicelines[Random.Range(0, CurrentSelectedVoicelines.Count)];
            CurrentSelectedVoicelines.Remove(clipToPlay);
            voiceLaneSource.clip = clipToPlay;
            _lastPlayedSelectedClips.Enqueue(clipToPlay);
            voiceLaneSource.Play();
        }
    }
}
