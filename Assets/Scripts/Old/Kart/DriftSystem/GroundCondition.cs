using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class GroundCondition : MonoBehaviour
    {
        [Header("State")]
        public bool Grounded;

        [Header("Parameters")]
        public float DistanceForGrounded;

        private void Update()
        {
            CheckGrounded();
        }

        private void CheckGrounded()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), DistanceForGrounded, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
            {
                Grounded = true;
            }
            else
            {
                Grounded = false;
            }
        }
    }
}
