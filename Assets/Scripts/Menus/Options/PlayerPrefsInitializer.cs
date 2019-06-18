using UnityEngine;
using UnityEngine.Audio;
using Multiplayer;

namespace Menu.Options
{
    [DisallowMultipleComponent]
    public class PlayerPrefsInitializer : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioMixer _audioMixer;
        private const string _masterSFXMixerName = "MasterSFX";
        private const string _masterMusicMixerName = "MasterMusic";

        [Header("Player")]
        [SerializeField] private PlayerSettings _playerSettings;

        // CORE

        private void Start()
        {
           InitializeAudio();
           InitializePlayerSettings();
        }

        // PRIVATE

        private void InitializeAudio()
        {
            _audioMixer.SetFloat(_masterSFXMixerName, PlayerPrefs.GetFloat(_masterSFXMixerName));
            _audioMixer.SetFloat(_masterMusicMixerName, PlayerPrefs.GetFloat(_masterMusicMixerName));
            Debug.Log("[PREFS] Initialized audio.");
        }

        private void InitializePlayerSettings()
        {
            _playerSettings.AbilityIndex = 1;
        }
    }
}
