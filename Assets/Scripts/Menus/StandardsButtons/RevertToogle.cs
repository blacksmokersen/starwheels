using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevertToogle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [Header ("initialization")]
    [SerializeField] private bool _initialize;
    [SerializeField] private bool _initializationValue;

    private void OnEnable()
    {
        if (_initialize)
        {
            _toggle.isOn = _initializationValue;
        }
    }

    public void RevertToggle()
    {
        _toggle.isOn = !_toggle.isOn;
    }
}
