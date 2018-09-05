using UnityEngine;

namespace Items
{
    public class ProjectileTrigger : MonoBehaviour
    {
        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.KartTriggerTag))
            {
                var projectileBehaviour = GetComponentInParent<ProjectileBehaviour>();
                projectileBehaviour.CheckCollision(other.gameObject);
            }
        }
    }
}
