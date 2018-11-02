using UnityEngine;
using Bolt;

namespace Items
{
    public class ProjectileTrigger : EntityBehaviour
    {
        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.HealthHitBox) && entity.isOwner)
            {
                var projectileBehaviour = GetComponentInParent<ProjectileBehaviour>();
                projectileBehaviour.CheckCollision(other.gameObject);
            }
        }
    }
}
