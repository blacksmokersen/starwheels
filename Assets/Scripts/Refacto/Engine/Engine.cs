using UnityEngine;

public class Engine : MonoBehaviour, IControllable
{
    [Header("Forces")]
    public EngineSettings Settings;

    [Header("Events")]
    public FloatEvent OnVelocityChange;

    private Rigidbody _rb;

	private void Awake ()
    {
        _rb = GetComponentInParent<Rigidbody>();
	}

	private void FixedUpdate ()
    {
        MapInputs();
        ClampMagnitude();
        OnVelocityChange.Invoke(_rb.velocity.magnitude);
	}

    private void ClampMagnitude()
    {
        if(Settings.MaxMagnitude > 0)
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, Settings.MaxMagnitude);
    }

    public void Accelerate(float value)
    {
        _rb.AddRelativeForce(Vector3.forward * value * Settings.SpeedForce, ForceMode.Force);
    }

    public void Decelerate(float value)
    {
        _rb.AddRelativeForce(Vector3.back * value * Settings.SpeedForce / Settings.DecelerationFactor, ForceMode.Force);
    }

    public void MapInputs()
    {
        Accelerate(Input.GetAxis(Constants.Input.Accelerate));
        Decelerate(Input.GetAxis(Constants.Input.Decelerate));
    }
}
