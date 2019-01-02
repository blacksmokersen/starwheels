using UnityEngine;
using Bolt;
using Multiplayer;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class KartTargetting : EntityBehaviour<IKartState>
    {
        public bool Targetting;

        [SerializeField] private GameObject _crossHairReference;
        [SerializeField] private GameObject _crossHair;
        [SerializeField] private Transform _origin;

        private Transform _currentTargetTransform;

        private void Update()
        {
            if (entity.isAttached && entity.isOwner && _currentTargetTransform != null)
            {
                _crossHair.transform.position = _currentTargetTransform.position + new Vector3(0f, 0.5f, 0f);
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

        public void Enable()
        {
            Targetting = true;
        }

        public void Disable()
        {
            Targetting = false;
            _crossHair.SetActive(false);
            _currentTargetTransform = null;
        }

        // PRIVATE

        private void SwitchTarget(Transform newTargetTransform)
        {
            _currentTargetTransform = newTargetTransform;
            _crossHair.SetActive(true);
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

                if (state.Team != otherPlayer.Team.GetColor())
                {
                    SwitchTarget(other.transform);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (Targetting && other.CompareTag(Constants.Tag.KartHealthHitBox))
            {
                if (_currentTargetTransform == null)
                {
                    SwitchTarget(other.transform);
                }
                else if (other.transform != _currentTargetTransform && IsCloserThanCurrentTarget(other.transform))
                {
                    var otherPlayer = other.GetComponentInParent<Player>();

                    if (state.Team != otherPlayer.Team.GetColor())
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
