using UnityEngine;

namespace Items
{
    public class RocketBehaviour : ProjectileBehaviour
    {
        [Header("Rocket parameters")]
        public float MaxAngle;
        public GameObject Target;

        private void LateUpdate()
        {
            if(Target != null)
            {

            }
        }

        public override void SetOwner(KartInventory kart)
        {
            transform.position = kart.ItemPositions.FrontPosition.position;
        }
    }
}