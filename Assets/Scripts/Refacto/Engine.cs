using UnityEngine;

public class Engine : MonoBehaviour, IControllable
{
    [Header("Forces")]
    public EngineSettings Settings;

    private Rigidbody _rb;

	private void Awake ()
    {
        _rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate ()
    {
        MapInputs();
	}

    public void Accelerate(float value)
    {
        _rb.AddRelativeForce(Vector3.forward * value * Settings.Speed, ForceMode.Force);
    }

    public void Decelerate(float value)
    {
        _rb.AddRelativeForce(Vector3.back * value * Settings.Speed / Settings.DecelerationFactor, ForceMode.Force);
    }

    public void MapInputs()
    {
        Accelerate(Input.GetAxis(Constants.Input.Accelerate));
        Decelerate(Input.GetAxis(Constants.Input.Decelerate));
    }
}
