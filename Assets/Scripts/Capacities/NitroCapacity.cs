using UnityEngine;

namespace Capacities
{
    public class NitroCapacity : MonoBehaviour, ICapacity
    {
        [Range(0,1)] public float Energy;

        public void Use(float xAxis, float yAxis)
        {
            throw new System.NotImplementedException();
        }
    }
}