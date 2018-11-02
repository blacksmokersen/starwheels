using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampSpeed : MonoBehaviour {

    [Header("BaseMaxSpeed")]
    public float ControlMaxSpeed;
    [Header("ActualMaxSpeed(Dont touch that)")]
    public float MaxSpeed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
        ControlMaxSpeed = MaxSpeed;
    }

    private void FixedUpdate()
    {
        ClampMagnitude();
    }

    private void ClampMagnitude()
    {
        if (MaxSpeed > 0)
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, MaxSpeed);
    }
}
