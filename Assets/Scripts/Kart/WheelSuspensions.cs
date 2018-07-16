using UnityEngine;

public class WheelSuspensions : MonoBehaviour {


    public float MaxExtensionDistance;
    public float SuspensionStiffness;
    public float ForceToApply;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, MaxExtensionDistance, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
        {
            var distance = Mathf.Clamp(hit.distance, 0, MaxExtensionDistance);
            var compressionRatio = - distance + MaxExtensionDistance;
            AdjustWheelPosition(compressionRatio);
        }
    }

    private void AdjustWheelPosition(float compressionRatio)
    {
        rb.AddForceAtPosition(ComputeForceToAdd(compressionRatio), transform.position, ForceMode.Acceleration);        
    }

    private Vector3 ComputeForceToAdd(float compressionRatio)
    {
        var newSuspensionForce = transform.TransformVector(Vector3.up * compressionRatio * SuspensionStiffness);
        return newSuspensionForce * ForceToApply;
    }
}
