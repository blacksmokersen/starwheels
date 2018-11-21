namespace Drift
{
    public class DriftAnimation : Bolt.EntityBehaviour<IKartState>
    {
        public void LeftDriftAnimation()
        {
            state.DriftLeft = true;
        }

        public void RightDriftAnimation()
        {
            state.DriftRight = true;
        }

        public void NoDriftAnimation()
        {
            state.DriftLeft = false;
            state.DriftRight = false;
        }
    }
}
