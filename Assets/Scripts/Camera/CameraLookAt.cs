using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void Update()
    {
        if (_target != null)
        transform.LookAt(_target);
    }
}
