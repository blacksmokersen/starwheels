using UnityEngine;

namespace FX
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailSystem : MonoBehaviour
    {
        public TrailRenderer Trail;

        private void Awake()
        {
            Trail = GetComponent<TrailRenderer>();            
        }

        public void SetUniqueColor(Color color)
        {
            Trail.startColor = color;
            Trail.endColor = color;
        }
    }
}