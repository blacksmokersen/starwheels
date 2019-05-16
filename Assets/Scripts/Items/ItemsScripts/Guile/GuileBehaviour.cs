namespace Items
{
    public class GuileBehaviour : ProjectileBehaviour
    {
        private void Start()
        {
            rb.useGravity = false;
        }

        // BOLT

        public override void Attached()
        {
            DestroyObject(10f); // In case item goes OOB
        }
    }
}
