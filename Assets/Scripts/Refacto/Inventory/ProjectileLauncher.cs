using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(ItemPositions))]
    public class ThrowableLauncher : MonoBehaviour
    {
        public Direction ThrowingDirection
        {
            get
            {
                float directionAxis = Input.GetAxis(Constants.Input.UpAndDownAxis);

                if (directionAxis > settings.ForwardTreshold)
                    return Direction.Forward;
                else if (directionAxis < settings.BackwardTreshold)
                    return Direction.Backward;
                else
                    return Direction.Default;
            }
        }

        [Header("LaunchType: Straight")]
        public float Speed;

        [Header("LaunchType: Arc")]
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;

        [SerializeField] private ProjectileLauncherSettings settings;

        private ItemPositions _itemPositions;

        // CORE

        private void Awake()
        {
            _itemPositions = GetComponent<ItemPositions>();
        }

        // PUBLIC

        public void Throw(Throwable throwable)
        {
            switch (throwable.ThrowableType)
            {
                case ThrowableType.Arc:
                    ArcThrow(throwable);
                    break;
                case ThrowableType.Straight:
                    StraightThrow(throwable);
                    break;
            }
        }

        // PRIVATE

        private void ArcThrow(Throwable throwable)
        {
            Transform transform = throwable.transform;
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            Vector3 rot = Vector3.zero;

            if (ThrowingDirection == Direction.Forward || ThrowingDirection == Direction.Default)
            {
                transform.position = _itemPositions.FrontPosition.position;
                rot = new Vector3(0, throwable.transform.rotation.eulerAngles.y, 0);
                var aimVector = throwable.transform.forward;
                rb.AddForce((aimVector + throwable.transform.up / TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
            else if (ThrowingDirection == Direction.Backward)
            {
                Drop(throwable);
            }
            transform.rotation = Quaternion.Euler(rot);
        }

        private void StraightThrow(Throwable throwable)
        {
            Transform transform = throwable.transform;
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            Vector3 rot = Vector3.zero;

            if (ThrowingDirection == Direction.Forward || ThrowingDirection == Direction.Default)
            {
                transform.position = _itemPositions.FrontPosition.position;
                rot = new Vector3(0, throwable.transform.rotation.eulerAngles.y, 0);
                rb.velocity = throwable.transform.forward.normalized * Speed;
            }
            else if (ThrowingDirection == Direction.Backward)
            {
                transform.position = _itemPositions.BackPosition.position;
                rot = new Vector3(0, throwable.transform.rotation.eulerAngles.y + 180, 0);
                rb.velocity = -throwable.transform.forward.normalized * Speed;
            }
            transform.rotation = Quaternion.Euler(rot);
        }

        private void Drop(Throwable throwable)
        {
            if (ThrowingDirection == Direction.Forward || ThrowingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
            }
            else if (ThrowingDirection == Direction.Backward)
            {
                throwable.transform.position = _itemPositions.BackPosition.position;
            }
        }
    }
}
