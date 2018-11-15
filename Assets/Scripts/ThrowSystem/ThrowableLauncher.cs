using UnityEngine;

namespace ThrowingSystem
{
    [RequireComponent(typeof(ThrowPositions))]
    public class ThrowableLauncher : Bolt.EntityBehaviour
    {
        [Header("LaunchType: Straight")]
        public float Speed;

        [Header("LaunchType: Arc")]
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;

        [SerializeField] Items.ShockwaveEffectBehaviour shockwaveEffectBehaviour;

        private ThrowPositions _itemPositions;



        // CORE

        private void Awake()
        {
            _itemPositions = GetComponent<ThrowPositions>();
        }

        // PUBLIC

        public Direction ThrowingDirection
        {
            get
            {
                float directionAxis = Input.GetAxis(Constants.Input.UpAndDownAxis);

                if (directionAxis > 0.3f)
                    return Direction.Forward;
                else if (directionAxis < -0.3f)
                    return Direction.Backward;
                else
                    return Direction.Default;
            }
        }

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
                case ThrowableType.Drop:
                    if (ThrowingDirection == Direction.Forward)
                    {
                        ArcThrow(throwable);
                    }
                    else
                    {
                        Drop(throwable);
                    }
                    break;
            }
        }

        // PRIVATE

        private void ArcThrow(Throwable throwable)
        {
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            Vector3 rot = Vector3.zero;

            if (ThrowingDirection == Direction.Forward || ThrowingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
                var aimVector = transform.forward;
                rb.AddForce((aimVector + transform.up / TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
            else if (ThrowingDirection == Direction.Backward)
            {
                Drop(throwable);
            }
            throwable.transform.rotation = Quaternion.Euler(rot);
            StartLaunchItemEvent(throwable.transform.position, throwable.transform.rotation, throwable.name);
        }

        private void StraightThrow(Throwable throwable)
        {
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            Vector3 rot = Vector3.zero;

            if (ThrowingDirection == Direction.Forward || ThrowingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
                rb.velocity = transform.forward.normalized * Speed;
            }
            else if (ThrowingDirection == Direction.Backward)
            {
                throwable.transform.position = _itemPositions.BackPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y + 180, 0);
                rb.velocity = -transform.forward.normalized * Speed;
            }
            throwable.transform.rotation = Quaternion.Euler(rot);
            StartLaunchItemEvent(throwable.transform.position, throwable.transform.rotation, throwable.name);
        }

        private void Drop(Throwable throwable)
        {
            Vector3 rot = Vector3.zero;
            if (ThrowingDirection == Direction.Forward)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
            }
            else if (ThrowingDirection == Direction.Backward || ThrowingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.BackPosition.position;
            }
            throwable.transform.rotation = Quaternion.Euler(rot);
            StartLaunchItemEvent(throwable.transform.position,throwable.transform.rotation,throwable.name);
        }

        private void StartLaunchItemEvent(Vector3 position,Quaternion rotation, string itemName)
        {
            //  shockwaveEffectBehaviour.InstantiateShockwave(throwableTransform);
            var launchEvent = PlayerLaunchItem.Create(entity);
            launchEvent.Position = position;
            launchEvent.Rotation = rotation;
            launchEvent.ItemName = itemName;
            launchEvent.Entity = GetComponentInParent<BoltEntity>();
            launchEvent.Send();

            //  var launchEvent = PlayerLaunchItem.Create(entity);
            //  launchEvent.Vector = throwableTransform.transform.position;
            //  launchEvent.Quaternion = throwable.transform.rotation;
            //   launchEvent.Send();
        }
    }
}
