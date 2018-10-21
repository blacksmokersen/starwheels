using UnityEngine;
using Items;

namespace Abilities
{
    [RequireComponent(typeof(LineRenderer))]
    public class HookBehaviour : MonoBehaviour
    {
        private enum HookState { Forward, Hooked, Reverse }

        [HideInInspector] public KartInventory OwnerKartInventory;

        [SerializeField] private float distanceTarget;
        [SerializeField] private float speed;

        private Transform _owner;
        private Transform _target;
        private GameObject _initialTargetObject;
        private Animator _animator;
        private HookState _state;
        private ItemBehaviour _itemBehaviour;
        private LineRenderer _lineRenderer;

        // CORE

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _animator = GetComponent<Animator>();

            var newTargetPosition = transform.position + transform.TransformDirection(new Vector3(0, 0, distanceTarget));
            _initialTargetObject = new GameObject("Hook Transform");
            _initialTargetObject.transform.position = newTargetPosition;
            _target = _initialTargetObject.transform;
        }

        private void Update()
        {
            StatesBehaviour();
            CheckIfIsHooked();
            CheckIfMissedTarget();
            UpdateLineRenderer();
        }

        // PUBLIC

        public void SetOwner(Transform owner)
        {
            _owner = owner;
        }

        // PRIVATE

        private void StatesBehaviour()
        {
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
        }

        private void OnTriggerEnter(Collider other)
        {
            // A wall
            if (other.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                _state = HookState.Reverse;
            }

            // All items
            else if (other.CompareTag(Constants.Tag.DiskItem)
                || other.CompareTag(Constants.Tag.RocketItem)
                || other.CompareTag(Constants.Tag.GuileItem)
                || other.CompareTag(Constants.Tag.GroundItem))
            {
                _itemBehaviour = other.GetComponentInParent<ItemBehaviour>();

                SetTarget(other.transform);
            }

            // ItemBox
            else if (other.CompareTag(Constants.Tag.ItemBox))
            {
                var itemBox = other.GetComponent<ItemBox>();
                itemBox.StartCoroutine(itemBox.StartCooldown());
                OwnerKartInventory.StartItemLottery();
                SetTarget(other.transform);
            }

            // Kart
            else if (other.CompareTag(Constants.Tag.KartTrigger))
            {
                /*
                var otherKartInventory = other.GetComponentInParent<Kart.KartHub>().kartInventory;
                if (otherKartInventory != OwnerKartInventory)
                {
                    OwnerKartInventory.SetItem(otherKartInventory.Item, otherKartInventory.Count);
                    otherKartInventory.SetItem(null, 0);
                    SetTarget(other.transform);
                }
                else if(otherKartInventory == OwnerKartInventory && _state == HookState.Reverse)
                {
                    //PhotonNetwork.Destroy(gameObject);
                }
                */
            }
        }

        private void CheckIfIsHooked()
        {
            if (_animator.GetNextAnimatorStateInfo(0).IsName("Reverse") && _state == HookState.Hooked)
            {
                _state = HookState.Reverse;
                /*
                if (_itemBehaviour)
                {
                    OwnerKartInventory.SetItem(_itemBehaviour.ItemData,1);
                    _itemBehaviour.DestroyObject();
                }
                */
            }
        }

        private void CheckIfMissedTarget()
        {
            if(_state == HookState.Forward && Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                _state = HookState.Reverse;
            }
        }

        private void MoveTowardsTarget()
        {
            if (_target)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
            }
        }

        private void MoveBackToOwner()
        {
            if (_owner)
            {
                transform.position = Vector3.MoveTowards(transform.position, _owner.position, speed * Time.deltaTime);
            }
        }

        private void UpdateLineRenderer()
        {
            if (_owner)
            {
                _lineRenderer.SetPosition(0, _owner.position);
                _lineRenderer.SetPosition(1, gameObject.transform.position);
            }
        }

        private void SetTarget(Transform newTarget)
        {
            _target = newTarget;
            _lineRenderer.SetPosition(1, _target.position);
            _state = HookState.Hooked;
            _animator.SetTrigger("Hit");
        }

        private void OnDestroy()
        {
            Destroy(_initialTargetObject.gameObject);
        }
    }
}
