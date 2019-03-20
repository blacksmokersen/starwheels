using UnityEngine;
using Bolt;
using Multiplayer;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class KartTargetting : EntityBehaviour<IKartState>
    {
        public string ItemName;

        public bool Targetting;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private GameObject _crossHairReference;
        [SerializeField] private Transform _origin;

        private GameObject _crossHair;
        private Transform _currentTargetTransform;
        private bool _needsDisabling = false;

        private void Update()
        {
            try
            {
                if (entity.isAttached && entity.isOwner)
                {
                    if (_currentTargetTransform != null)
                    {
                        _crossHair.transform.position = _currentTargetTransform.position + new Vector3(0f, 0.5f, 0f);
                    }
                    else if (_currentTargetTransform == null && _needsDisabling)
                    {
                        UnsetTarget();
                    }
                }
            }
            catch (MissingReferenceException)
            {
                Debug.Log("Missing reference.");
                Disable();
            }
        }

        // BOLT

        public override void Attached()
        {
            if (entity.isOwner)
            {
                _crossHair = Instantiate(_crossHairReference, transform, true);
                _crossHair.SetActive(false);
            }
        }

        // PUBLIC

        public void CheckIfItemNeedsTarget(Item item)
        {
            if (item != null && item.Name == ItemName)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }

        public void CheckIfNoMoreAmmo(int count)
        {
            if (count == 0)
            {
                Disable();
            }
        }

        public void Enable()
        {
            Targetting = true;
        }

        public void Disable()
        {
            Targetting = false;
            UnsetTarget();
        }

        // PRIVATE

        private void SwitchTarget(Transform newTargetTransform)
        {
            _currentTargetTransform = newTargetTransform;
            _crossHair.SetActive(true);
            _needsDisabling = true;
        }

        private void UnsetTarget()
        {
            _currentTargetTransform = null;
            if (_crossHair)
            {
                _crossHair.SetActive(false);
            }
            _needsDisabling = false;
        }

        private bool IsCloserThanCurrentTarget(Transform target)
        {
            return Vector3.Distance(_origin.position, target.position) < Vector3.Distance(_origin.position, _currentTargetTransform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Targetting && other.CompareTag(Constants.Tag.KartHealthHitBox) && _currentTargetTransform == null)
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (state.Team.ToTeam() != otherPlayer.Team)
                {
                    SwitchTarget(other.transform);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (Targetting && other.CompareTag(Constants.Tag.KartHealthHitBox))
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (entity.isAttached && state.Team.ToTeam() != otherPlayer.Team)
                {
                    if (_currentTargetTransform == null)
                    {
                        SwitchTarget(other.transform);
                    }
                    else if (other.transform != _currentTargetTransform && IsCloserThanCurrentTarget(other.transform))
                    {
                        SwitchTarget(other.transform);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartHealthHitBox) && other.transform == _currentTargetTransform)
            {
                SwitchTarget(null);
                _crossHair.SetActive(false);
            }
        }
    }
}
