using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeredAudioPlayer : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource _baseLayerAudioSource;
    [SerializeField] private AudioSource[] _LayersAudioSource;

    private bool _searchingforBaseLayerToEnd = false;

    private void Awake()
    {
        _baseLayerAudioSource.Play();
    }

    private void Update()
    {
        if (_searchingforBaseLayerToEnd)
        {

        }
    }



    public void PlayNextLayerWhenBaseLayerEnd()
    {

    }

}
