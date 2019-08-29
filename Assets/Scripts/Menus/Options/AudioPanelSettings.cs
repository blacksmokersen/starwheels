using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menu.Options
{
    [DisallowMultipleComponent]
    public class AudioPanelSettings : MonoBehaviour
    {
        [Header("Audio Panel Settings")]
        [SerializeField] private AudioMixer _audioMixer;

        [Header("UI Elements")]
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _menuSoundsSlider;
        [SerializeField] private Slider _voiceSlider;
        [SerializeField] private Slider _advertSlider;

        private const string _masterMixerName = "Master";
        private const string _masterSFXMixerName = "MasterSFX";
        private const string _masterMusicMixerName = "MasterMusic";
        private const string _masterMenuSoundsMixerName = "MenuSounds";
        private const string _masterVoiceLinesMixerName = "VoiceLines";
        private const string _masterAdvertMixerName = "VoiceLinesGameModes";

        // CORE

        private void OnEnable()
        {
            InitializeUI();
        }

        private void OnDisable()
        {
            PlayerPrefs.Save(); // Save values when the panel closes
        }

        // PUBLIC

        public void SetMasterVolume(float value)
        {
            _audioMixer.SetFloat(_masterMixerName, value);
            PlayerPrefs.SetFloat(_masterMixerName, value);
        }

        public void SetSFXVolume(float value)
        {
            _audioMixer.SetFloat(_masterSFXMixerName, value);
            PlayerPrefs.SetFloat(_masterSFXMixerName, value);
        }

        public void SetMusicVolume(float value)
        {
            _audioMixer.SetFloat(_masterMusicMixerName, value);
            PlayerPrefs.SetFloat(_masterMusicMixerName, value);
        }

        public void SetHUDVolume(float value)
        {
            Debug.Log(value);
            _audioMixer.SetFloat(_masterMenuSoundsMixerName, value);
            PlayerPrefs.SetFloat(_masterMenuSoundsMixerName, value);
        }

        public void SetVoicesVolume(float value)
        {
            _audioMixer.SetFloat(_masterVoiceLinesMixerName, value);
            PlayerPrefs.SetFloat(_masterVoiceLinesMixerName, value);
        }

        public void SetAdvertVolume(float value)
        {
            _audioMixer.SetFloat(_masterAdvertMixerName, value);
            PlayerPrefs.SetFloat(_masterAdvertMixerName, value);
        }

        // PRIVATE

        private void InitializeUI()
        {
            _masterSlider.value = PlayerPrefs.GetFloat(_masterMixerName);
            _sfxSlider.value = PlayerPrefs.GetFloat(_masterSFXMixerName);
            _musicSlider.value = PlayerPrefs.GetFloat(_masterMusicMixerName);
            _menuSoundsSlider.value = PlayerPrefs.GetFloat(_masterMenuSoundsMixerName);
            _voiceSlider.value = PlayerPrefs.GetFloat(_masterVoiceLinesMixerName);
            _advertSlider.value = PlayerPrefs.GetFloat(_masterAdvertMixerName);
        }
    }
}
