using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMultiPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _sounds;

    public void PlaySound(int soundID)
    {
        if (_sounds.Length > soundID)
        {
            if (_sounds[soundID] != null)
            {
                _source.clip = _sounds[soundID];
                _source.Play();
            }
        }
    }
}
