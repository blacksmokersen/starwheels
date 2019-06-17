using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeredAudioPlayer : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource _baseLayerAudioSource;
    [SerializeField] private AudioSource[] _LayersAudioSource;

    private bool _searchingforBaseLayerToEnd = false;
    private bool _addLayer = false;

    private List<AudioSource> _layersToStart;
    private List<AudioSource> _layersToStop;

    private int _layerBuffer;

    private void Awake()
    {
        _layersToStart = new List<AudioSource>();
        _layersToStop = new List<AudioSource>();
        _baseLayerAudioSource.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
            PlaySpecificLayerWhenBaseLayerEnd(0);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            PlaySpecificLayerWhenBaseLayerEnd(1);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            RemoveSpecificLayerWhenBaseLayerEnd(0);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            RemoveSpecificLayerWhenBaseLayerEnd(1);



        if (_baseLayerAudioSource.time >= _baseLayerAudioSource.clip.length)
        {
            OnBaseLayerEnd();

            if (_searchingforBaseLayerToEnd)
            {
                LayersSetter(_layerBuffer, _addLayer);
                _searchingforBaseLayerToEnd = false;
            }
        }
    }

    public void PlaySpecificLayerWhenBaseLayerEnd(int layerNumber)
    {
        _layersToStart.Add(_LayersAudioSource[layerNumber]);
        _searchingforBaseLayerToEnd = true;
    }

    public void RemoveSpecificLayerWhenBaseLayerEnd(int layerNumber)
    {
        _layersToStop.Add(_LayersAudioSource[layerNumber]);
        _searchingforBaseLayerToEnd = true;
    }

    private void LayersSetter(int layerNumber, bool addLayer)
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
                layertoFade.volume -= 0.01f;
            _currentTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        layertoFade.Stop();
    }
}
