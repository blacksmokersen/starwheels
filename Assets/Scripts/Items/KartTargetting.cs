using UnityEngine;
using Bolt;
using Multiplayer;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class KartTargetting : EntityBehaviour<IKartState>
    {
        public string ItemName;

        public Transform CurrentTargetTransform;
        public bool Targetting;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private GameObject _crossHairReference;
        [SerializeField] private Transform _origin;

        private GameObject _crossHair;
        private bool _needsDisabling = false;

        // CORE

        private void Update()
        {
            try
            {
                if (entity.isAttached && entity.isOwner)
                {
                    if (CurrentTargetTransform != null)
                    {
                        _crossHair.transform.position = CurrentTargetTransform.position + new Vector3(0f, 0.5f, 0f);
                    }
                    else if (CurrentTargetTransform == null && _needsDisabling)
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
            CurrentTargetTransform = newTargetTransform;
            _crossHair.SetActive(true);
            _needsDisabling = true;
        }

        private void UnsetTarget()
        {
            CurrentTargetTransform = null;
            if (_crossHair)
            {
                _crossHair.SetActive(false);
            }
            _needsDisabling = false;
        }

        private bool IsCloserThanCurrentTarget(Transform target)
        {
            return Vector3.Distance(_origin.position, target.position) < Vector3.Distance(_origin.position, CurrentTargetTransform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Targetting && other.CompareTag(Constants.Tag.KartHealthHitBox) && CurrentTargetTransform == null)
            {
                var otherPlayer = other.GetComponentInParent<PlayerInfo>();

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
                var otherPlayer = other.GetComponentInParent<PlayerInfo>();

                if (entity.isAttached && state.Team.ToTeam() != otherPlayer.Team)
                {
                    if (CurrentTargetTransform == null)
                    {
                        SwitchTarget(other.transform);
                    }
                    else if (other.transform != CurrentTargetTransform && IsCloserThanCurrentTarget(other.transform))
                    {
                        SwitchTarget(other.transform);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartHealthHitBox) && other.transform == CurrentTargetTransform)
            {
                SwitchTarget(null);
                _crossHair.SetActive(false);
            }
        }
    }
}
