using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public GameObject ActualCamera;

    [SerializeField] private GameObject _kartCamera;
    [SerializeField] private GameObject _mapCamera;
    [SerializeField] private GameObject _ionbeamCamera;

    private void Awake()
    {
        _mapCamera = GameObject.FindWithTag("MapCamera");
    }



}
