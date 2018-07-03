using UnityEngine;

public class WheelSuspensions : MonoBehaviour {


    public float MaxExtensionDistance;
    public float SuspensionStiffness;
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
        CheckGround();
        Debug.Log(transform.InverseTransformDirection(rb.GetPointVelocity(transform.position)).y);

    }

    private void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, MaxExtensionDistance, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
        {
            var distance = Mathf.Clamp(hit.distance, 0, MaxExtensionDistance);
            var compressionRatio = - distance + MaxExtensionDistance;
            Debug.Log("Compression Ratio : " + compressionRatio);
            AdjustWheelPosition(compressionRatio);
        }
    }

    private void AdjustWheelPosition(float compressionRatio)
    {
        rb.AddForceAtPosition(ComputeForceToAdd(compressionRatio), transform.position, ForceMode.Acceleration);        
    }

    private Vector3 ComputeForceToAdd(float compressionRatio)
    {
        //var velocityUpKart = new Vector3(0,transform.InverseTransformDirection(rb.GetPointVelocity(transform.position)).y,0);
        var newSuspensionForce = transform.TransformVector(Vector3.up * compressionRatio * SuspensionStiffness);
        //var deltaForce = newSuspensionForce - velocityUpKart;

        return newSuspensionForce * ForceToApply;
    }
}
