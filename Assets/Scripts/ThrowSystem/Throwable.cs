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

    public enum Size
    {
        Small,
        Medium,
        Big
    }

    [RequireComponent(typeof(Rigidbody))]
    public class Throwable : EntityBehaviour<IThrowableState>
    {
        public ThrowableType ThrowableType;
        public Direction DefaultThrowingDirection;
        public Direction ForwardInputThrowingDirection;
        public Direction BackwardInputThrowingDirection;
        public Size ThrowableSize;
        public FloatVariable Speed;

        [Header("Temporal Anti-Aliasing")]
        [SerializeField] private Transform _throwableMeshesTransform;

        // BOLT

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform, _throwableMeshesTransform);
        }
    }
}
