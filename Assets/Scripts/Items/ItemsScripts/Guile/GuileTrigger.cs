namespace Items
{
    public class GuileTrigger : ProjectileTrigger
    {
        private new void OnTriggerEnter(UnityEngine.Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.CompareTag(Constants.GroundItemTag) ||
                other.gameObject.CompareTag(Constants.DiskItemTag) ||
                other.gameObject.CompareTag(Constants.RocketItemTag))
            {
                other.gameObject.GetComponentInParent<ItemBehaviour>().DestroyObject();
            }
            else if (other.gameObject.CompareTag(Constants.GuileItemTag))
            {
                GetComponentInParent<ItemBehaviour>().DestroyObject();
            }
        }
    }
}
