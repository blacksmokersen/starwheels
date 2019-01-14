using UnityEngine;

namespace ThrowingSystem
{
    [RequireComponent(typeof(ThrowPositions))]
    public class ThrowableLauncher : Bolt.EntityBehaviour
    {
        [Header("LaunchType: Arc")]
        public float TimesLongerThanHighThrow;

        private ThrowPositions _itemPositions;

        // CORE

        private void Awake()
        {
            _itemPositions = GetComponent<ThrowPositions>();
        }

        // PUBLIC

        public void Throw(Throwable throwable, Direction throwingDirection)
        {
            switch (throwable.ThrowableType)
            {
                case ThrowableType.Arc:
                    ArcThrow(throwable, throwingDirection);
                    break;
                case ThrowableType.Straight:
                    StraightThrow(throwable, throwingDirection);
                    break;
                case ThrowableType.Drop:
                    if (throwingDirection == Direction.Forward)
                    {
                        ArcThrow(throwable, throwingDirection);
                    }
                    else
                    {
                        Drop(throwable, throwingDirection);
                    }
                    break;
            }
        }

        // PRIVATE

        private void ArcThrow(Throwable throwable, Direction throwingDirection)
        {
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            Vector3 rot = Vector3.zero;

            if (throwingDirection == Direction.Forward || throwingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
                var aimVector = transform.forward + transform.up / TimesLongerThanHighThrow;
                rb.AddForce(aimVector  * throwable.Speed.Value, ForceMode.Impulse);
            }
            else if (throwingDirection == Direction.Backward)
            {
                Drop(throwable, throwingDirection);
            }
            throwable.transform.rotation = Quaternion.Euler(rot);
            StartLaunchItemEvent(throwable.transform.position, throwable.transform.rotation, throwable.name);
        }

        private void StraightThrow(Throwable throwable, Direction throwingDirection)
        {
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            Vector3 rot = Vector3.zero;

            if (throwingDirection == Direction.Forward || throwingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
                rb.velocity = transform.forward.normalized * throwable.Speed.Value;
            }
            else if (throwingDirection == Direction.Backward)
            {
                throwable.transform.position = _itemPositions.BackPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y + 180, 0);
                rb.velocity = -transform.forward.normalized * throwable.Speed.Value;
            }
            throwable.transform.rotation = Quaternion.Euler(rot);
            StartLaunchItemEvent(throwable.transform.position, throwable.transform.rotation, throwable.name);
        }

        private void Drop(Throwable throwable, Direction throwingDirection)
        {
            Vector3 rot = Vector3.zero;
            if (throwingDirection == Direction.Forward)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
            }
            else if (throwingDirection == Direction.Backward || throwingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.BackPosition.position;
            }
            throwable.transform.rotation = Quaternion.Euler(rot);
            StartLaunchItemEvent(throwable.transform.position,throwable.transform.rotation,throwable.name);
        }

        private void StartLaunchItemEvent(Vector3 position,Quaternion rotation, string itemName)
        {
            var launchEvent = PlayerLaunchItem.Create(entity);
            launchEvent.Position = position;
            launchEvent.Rotation = rotation;
            launchEvent.ItemName = itemName;
            launchEvent.Entity = GetComponentInParent<BoltEntity>();
            launchEvent.Send();
        }
    }
}
