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
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;

        private const string _masterSFXMixerName = "MasterSFX";
        private const string _masterMusicMixerName = "MasterMusic";

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

        // PRIVATE

        private void InitializeUI()
        {
            _sfxSlider.value = PlayerPrefs.GetFloat(_masterSFXMixerName);
            _musicSlider.value = PlayerPrefs.GetFloat(_masterMusicMixerName);
        }
    }
}
