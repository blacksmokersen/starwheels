using UnityEngine;

public class CustomDrag : MonoBehaviour
{
    public float Drag;
    public float AngularDrag;
    public float SlipCompensationForce;

    private Rigidbody _rb;
    private float _initialDrag;
    private float _initialAngularDrag;

    // CORE

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CompensateSlip();
    }

    // PUBLIC

    public void SetCustomDrag()
    {
        _initialDrag = _rb.drag;
        _rb.drag = Drag;
    }

    public void SetCustomAngularDrag()
    {
        _initialAngularDrag = _rb.angularDrag;
        _rb.angularDrag = AngularDrag;
    }

    public void ResetToDefaultDrag()
    {
        _rb.drag = _initialDrag;
    }

    public void ResetToDefaultAngularDrag()
    {
        _rb.angularDrag = _initialAngularDrag;
    }

    // PRIVATE

    private void CompensateSlip()
    {
        var sideVelocity = new Vector3(transform.InverseTransformDirection(_rb.velocity).x, 0, 0);
        _rb.AddRelativeForce(-sideVelocity * SlipCompensationForce, ForceMode.Force);
    }
}
