using UnityEngine;
using Bolt;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class Throwable : EntityBehaviour<IThrowableState>
    {
        public ThrowableType ThrowableType;

        // BOLT

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
        }
    }
}
