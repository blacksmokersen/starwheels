using System.Collections.Generic;
using UnityEngine;

namespace Drift
{
    public class DriftTrailController : MonoBehaviour
    {
        [SerializeField] private List<TrailRenderer> _groundTrails;

        private void Start()
        {
            SetTrailsActive(false);
        }

        public void SetTrailsActive(bool b)
        {
            foreach(var trail in _groundTrails)
            {
                trail.emitting = b;
            }
        }
    }
}
