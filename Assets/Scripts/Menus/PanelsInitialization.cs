using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelsInitialization : MonoBehaviour
{
    [SerializeField] private GameObject[] _openedPanels;
    [SerializeField] private GameObject[] _closedPanels;

    public UnityEvent InitializationEvent;


    public void Initialization()
    {
        foreach (GameObject _panel in _openedPanels)
        {
            _panel.SetActive(true);
        }
        foreach (GameObject _panel in _closedPanels)
        {
            _panel.SetActive(false);
        }

        if (InitializationEvent != null)
        {
            InitializationEvent.Invoke();
        }
    }
}
