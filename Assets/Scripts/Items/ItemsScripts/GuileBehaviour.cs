namespace Items
{
    public class GuileBehaviour : ProjectileBehaviour
    {        
        new void Start()
        {
            base.Start();
            rb.useGravity = false;
        }

        // We override it because we don't want to call CheckGrounded
        new void Update()
        {

        }

        private new void OnTriggerEnter(UnityEngine.Collider other)
        {
            base.OnTriggerEnter(other);
            if(other.gameObject.CompareTag(Constants.GroundItemTag) ||
                other.gameObject.CompareTag(Constants.ProjectileTag))
            {
                other.gameObject.GetComponentInParent<ItemBehaviour>().DestroyObject();
            }
        }
    }
}