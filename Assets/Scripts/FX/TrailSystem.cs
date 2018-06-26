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

        public void HideTrail()
        {
            var endColor = Trail.endColor;
            var startColor = Trail.startColor;
            endColor.a = 0f;
            startColor.a = 0f;
            Trail.endColor = endColor;
            Trail.startColor = startColor;
        }

        public void ShowTrail()
        {
            var endColor = Trail.endColor;
            var startColor = Trail.startColor;
            endColor.a = 1f;
            startColor.a = 1f;
            Trail.endColor = endColor;
            Trail.startColor = startColor;
        }
    }
}