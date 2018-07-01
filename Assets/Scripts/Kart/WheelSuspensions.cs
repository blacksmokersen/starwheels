using UnityEngine;

public class WheelSuspensions : MonoBehaviour {

    public float CompressionRatio;
    public float MaxExtensionDistance;
    public float StayDistance;
    public float ForceToApply;

    private Vector3 initialPosition;
    private Rigidbody rb;

    private void Awake()
    {
        initialPosition = transform.localPosition;
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        AdjustWheelPosition(DistanceFromGround());
    }

    private float DistanceFromGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,  MaxExtensionDistance, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
        {
            return Mathf.Clamp(hit.distance, 0, MaxExtensionDistance);
        }
        else
        {
            return MaxExtensionDistance;
        }
    }

    private void AdjustWheelPosition(float distance)
    {
        rb.AddForceAtPosition(ComputeForceToAdd(distance), transform.position, ForceMode.Force);        
    }

    private Vector3 ComputeForceToAdd(float distance)
    {
        return new Vector3(0, (-distance + MaxExtensionDistance) * ForceToApply, 0);
    }
}
