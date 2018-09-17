using UnityEngine;
using Items;

namespace Abilities
{
    public class HookBehaviour : MonoBehaviour
    {
        private enum HookState { Forward, Hooked, Reverse }

        [HideInInspector] public KartInventory OwnerKartInventory;
        [HideInInspector] public Transform Owner;

        [SerializeField] private float distanceTarget;
        [SerializeField] private float speed;

        private Transform _target;
        private Animator _animator;
        private HookState _state;
        private ItemBehaviour _itemBehaviour;

        // CORE

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            var newTargetPosition = transform.position + transform.TransformDirection(new Vector3(0, 0, distanceTarget));
            _target = new GameObject("Hook Transform").transform;
            _target.position = newTargetPosition;
        }

        private void Update()
        {
            Debug.Log("Target : " + _target);
            switch (_state)
            {
                case HookState.Forward:
                    MoveTowardsTarget();
                    break;
                case HookState.Hooked:
                    MoveTowardsTarget();
                    break;
                case HookState.Reverse:
                    MoveBackToOwner();
                    break;
            }

            if (_animator.GetNextAnimatorStateInfo(0).IsName("Reverse") && _state == HookState.Hooked)
            {
                _state = HookState.Reverse;
                OwnerKartInventory.SetItem(_itemBehaviour.ItemData);
                OwnerKartInventory.SetCount(1);
                _itemBehaviour.DestroyObject();
            }
        }

        // PRIVATE

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                _state = HookState.Reverse;
            }

            else if (other.CompareTag(Constants.Tag.DiskItem)
                || other.CompareTag(Constants.Tag.RocketItem)
                || other.CompareTag(Constants.Tag.GuileItem)
                || other.CompareTag(Constants.Tag.GroundItem))
            {
                _itemBehaviour = other.GetComponentInParent<ItemBehaviour>();

                SetTarget(other.transform);
                _animator.SetTrigger("Hit");
            }

            else if (other.CompareTag(Constants.Tag.KartTrigger))
            {
                var otherKartInventory = other.GetComponentInParent<Kart.KartHub>().kartInventory;
                if (otherKartInventory != OwnerKartInventory)
                {
                    OwnerKartInventory.Item = otherKartInventory.Item;
                    OwnerKartInventory.Count = otherKartInventory.Count;
                    SetTarget(other.transform);
                    _animator.SetTrigger("Hit");
                }
                else if(otherKartInventory == OwnerKartInventory && _state == HookState.Reverse)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void MoveTowardsTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
        }

        private void MoveBackToOwner()
        {
            transform.position = Vector3.MoveTowards(transform.position, Owner.position, speed * Time.deltaTime);
        }

        private void SetTarget(Transform newTarget)
        {
            _target = newTarget;
            _state = HookState.Hooked;
        }

        private void OnDestroy()
        {
            Destroy(_target);
        }
    }
}
