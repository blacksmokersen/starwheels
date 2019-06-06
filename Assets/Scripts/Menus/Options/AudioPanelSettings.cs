using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Menu.Options
{
    [DisallowMultipleComponent]
    public class AudioPanelSettings : MonoBehaviour
    {
        [Header("Audio Panel Settings")]
        [SerializeField] private AudioMixer audioMixer;

        // PUBLIC

        public void SetSFXVolume(float value)
        {
            audioMixer.SetFloat("MasterSFX", value);
        }

        public void SetMusicVolume(float value)
        {
            audioMixer.SetFloat("MasterMusic", value);
        }
    }
}
