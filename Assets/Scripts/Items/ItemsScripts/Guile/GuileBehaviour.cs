namespace Items
{
    public class GuileBehaviour : ProjectileBehaviour
    {
        private new void Start()
        {
            base.Start();
            rb.useGravity = false;
            DestroyObject(10f);
        }

        // We override it because we don't want to call CheckGrounded
        new void Update() { }
    }
}
