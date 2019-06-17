using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class LayeredAudioPlayer : GlobalEventListener
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource _baseLayerAudioSource;
    [SerializeField] private AudioSource[] _LayersAudioSource;

    private bool _searchingforBaseLayerToEnd = false;

    private List<AudioSource> _layersToStart;
    private List<AudioSource> _layersToStop;

    //CORE

    private void Awake()
    {
        _layersToStart = new List<AudioSource>();
        _layersToStop = new List<AudioSource>();
        _baseLayerAudioSource.Play();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha6))
            PlaySpecificLayerWhenBaseLayerEnd(0);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            PlaySpecificLayerWhenBaseLayerEnd(1);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            RemoveSpecificLayerWhenBaseLayerEnd(0);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            RemoveSpecificLayerWhenBaseLayerEnd(1);
            */


        if (_baseLayerAudioSource.time >= _baseLayerAudioSource.clip.length)
        {
            OnBaseLayerEnd();

            if (_searchingforBaseLayerToEnd)
            {
                LayersSetter();
                _searchingforBaseLayerToEnd = false;
            }
        }
        if (!_baseLayerAudioSource.isPlaying)
            _baseLayerAudioSource.Play();
    }

    //BOLT

    public override void OnEvent(ScoreIncreased evnt)
    {
        if(evnt.Score == 7)
        {
            PlaySpecificLayerWhenBaseLayerEnd(0);
        }
        if (evnt.Score == 10)
        {
            PlaySpecificLayerWhenBaseLayerEnd(1);
        }
        if(evnt.Score == 13)
        {
            RemoveSpecificLayerWhenBaseLayerEnd(0);
            RemoveSpecificLayerWhenBaseLayerEnd(1);
        }
    }

    //PUBLIC

    public void PlaySpecificLayerWhenBaseLayerEnd(int layerNumber)
    {
        Debug.Log("Added Layer Number : " + layerNumber);
        _layersToStart.Add(_LayersAudioSource[layerNumber]);
        _searchingforBaseLayerToEnd = true;
    }

    public void RemoveSpecificLayerWhenBaseLayerEnd(int layerNumber)
    {
        Debug.Log("Removed Layer Number : " + layerNumber);
        _layersToStop.Add(_LayersAudioSource[layerNumber]);
        _searchingforBaseLayerToEnd = true;
    }

    //PRIVATE

    private void LayersSetter()
    {
        foreach (AudioSource layer in _layersToStart)
        {
            layer.Play();
            layer.volume = 1;
        }
        foreach (AudioSource layer in _layersToStop)
        {
            StartCoroutine(FadingLayerToRemove(layer, 5));
        }

        _layersToStart.Clear();
        _layersToStop.Clear();
    }

    private void OnBaseLayerEnd()
    {
        _baseLayerAudioSource.Play();
    }

    IEnumerator FadingLayerToRemove(AudioSource layertoFade, float fadeDuration)
    {
        var _currentTimer = 0f;
        while (_currentTimer < fadeDuration)
        {
            if (layertoFade.volume > 0)
                layertoFade.volume -= 0.1f;
            _currentTimer += 0.5f;
            yield return new WaitForSeconds(0.5f);
            // _currentTimer += Time.fixedDeltaTime;
            // yield return new WaitForFixedUpdate();
        }
        layertoFade.Stop();
    }
}
