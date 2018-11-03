﻿using UnityEngine;

namespace ThrowingSystem
{
    [RequireComponent(typeof(ThrowPositions))]
    public class ThrowableLauncher : MonoBehaviour
    {
        [Header("LaunchType: Straight")]
        public float Speed;

        [Header("LaunchType: Arc")]
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;


        private ThrowPositions _itemPositions;
        private Direction _throwingDirection
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

        // CORE

        private void Awake()
        {
            _itemPositions = GetComponent<ThrowPositions>();
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
                case ThrowableType.Drop:
                    if (_throwingDirection == Direction.Forward)
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

            if (_throwingDirection == Direction.Forward || _throwingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
                var aimVector = transform.forward;
                rb.AddForce((aimVector + transform.up / TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
            else if (_throwingDirection == Direction.Backward)
            {
                Drop(throwable);
            }
            throwable.transform.rotation = Quaternion.Euler(rot);
        }

        private void StraightThrow(Throwable throwable)
        {
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            Vector3 rot = Vector3.zero;

            if (_throwingDirection == Direction.Forward || _throwingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
                rb.velocity = transform.forward.normalized * Speed;
            }
            else if (_throwingDirection == Direction.Backward)
            {
                throwable.transform.position = _itemPositions.BackPosition.position;
                rot = new Vector3(0, transform.rotation.eulerAngles.y + 180, 0);
                rb.velocity = -transform.forward.normalized * Speed;
            }
            throwable.transform.rotation = Quaternion.Euler(rot);
        }

        private void Drop(Throwable throwable)
        {
            if (_throwingDirection == Direction.Forward)
            {
                throwable.transform.position = _itemPositions.FrontPosition.position;
            }
            else if (_throwingDirection == Direction.Backward || _throwingDirection == Direction.Default)
            {
                throwable.transform.position = _itemPositions.BackPosition.position;
            }
        }
    }
}