using UnityEngine;

public class GroundCondition : MonoBehaviour
{
    [Header("State")]
    public bool Grounded;

    [Header("Parameters")]
    [SerializeField] private float distanceForGrounded;

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), distanceForGrounded, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }
    }
}
