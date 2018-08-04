using UnityEngine;

public class WheelSuspensions : MonoBehaviour {


    public float MaxExtensionDistance;
    public float SuspensionStiffness;
    public float ForceToApply;

    [Header("Wheels transforms")]
    public Transform[] wheelsTransforms;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach (var wheelTransform in wheelsTransforms)
        {
            AdjustWheelPosition(wheelTransform);
        }
    }

    private void AdjustWheelPosition(Transform wheelTransform)
    {
        RaycastHit hit;
        if (Physics.Raycast(wheelTransform.position, Vector3.down, out hit, MaxExtensionDistance, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
        {
            var distance = Mathf.Clamp(hit.distance, 0, MaxExtensionDistance);
            var compressionRatio = - distance + MaxExtensionDistance;
            rb.AddForceAtPosition(ComputeForceToAdd(compressionRatio), wheelTransform.position, ForceMode.Acceleration);
        }
    }

    private Vector3 ComputeForceToAdd(float compressionRatio)
    {
        var newSuspensionForce = transform.TransformVector(Vector3.up * compressionRatio * SuspensionStiffness);
        return newSuspensionForce * ForceToApply;
    }
}
