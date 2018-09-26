using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomDrag : MonoBehaviour
{
    public float Drag;
    public float AngularDrag;

    private Rigidbody _rb;
    private float _initialDrag;
    private float _initialAngularDrag;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

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
}
