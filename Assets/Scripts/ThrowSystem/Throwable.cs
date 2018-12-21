using UnityEngine;
using Bolt;

namespace ThrowingSystem
{
    public enum ThrowableType
    {
        Straight,
        Arc,
        Drop,
        None
    }

    [RequireComponent(typeof(Rigidbody))]
    public class Throwable : EntityBehaviour<IThrowableState>
    {
        public ThrowableType ThrowableType;
        public FloatVariable Speed;

        // BOLT

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
        }
    }
}
