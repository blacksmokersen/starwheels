namespace Network
{
    public class KartNetworkBehaviour : Bolt.EntityBehaviour<IKartState>
    {
        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
        }
    }
}
