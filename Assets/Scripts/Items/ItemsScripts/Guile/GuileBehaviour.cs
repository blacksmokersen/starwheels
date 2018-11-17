namespace Items
{
    public class GuileBehaviour : ProjectileBehaviour
    {
        private new void Start()
        {
            base.Start();
            rb.useGravity = false;            
        }

        // We override it because we don't want to call CheckGrounded
        new void Update() { }

        public override void ControlGained()
        {
            if (entity.isOwner)
            {
                BoltEntity.Destroy(entity, 10f);
            }
        }
    }
}
