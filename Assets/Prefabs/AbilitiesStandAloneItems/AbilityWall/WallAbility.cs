using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Common.PhysicsUtils;
using Bolt;

namespace Abilities
{
    public class WallAbility : Ability, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Events")]
        public UnityEvent OnWallPreview;
        public UnityEvent OnWallSpawn;

        [Header("Conditions")]
        [SerializeField] private GroundCondition _groundCondition;

        [Header("WallPrefab")]
        [SerializeField] private GameObject _wallAbilityPrefab;
        [SerializeField] private GameObject _wallPreviewRaycastOrigin;
        [SerializeField] private GameObject _wallPreview;

        private bool _enableWallPreview = false;

        //CORE

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner && gameObject.activeInHierarchy)
            {
                MapInputs();

                if (_enableWallPreview)
                {
                    MovePreviewRaycast();
                    PreviewWall();
                }
            }
        }

        //PUBLIC

        public void MapInputs()
        {
            if (Enabled && Input.GetButtonDown(Constants.Input.UseAbility))
            {
                _wallPreview.SetActive(true);
                _enableWallPreview = true;
            }
            if (Enabled && Input.GetButtonUp(Constants.Input.UseAbility))
            {
                InstantiateWall();
            }
        }

        public new void Reload()
        {
            CanUseAbility = true;
        }

        //PRIVATE

        private void JumpToTopOfThePrefab()
        {

        }

        private void MovePreviewRaycast()
        {
            var leftJoytstickInput = Input.GetAxis(Constants.Input.UpAndDownAxis);
            _wallPreviewRaycastOrigin.transform.localPosition = Vector3.forward * (leftJoytstickInput * 10);
        }

        private void PreviewWall()
        {
            if (entity.isOwner)
            {
                RaycastHit hit;
                if (Physics.Raycast(_wallPreviewRaycastOrigin.transform.position, Vector3.down, out hit, 100, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
                {
                    Quaternion instantiateRotation = _wallPreview.transform.rotation;
                    _wallPreview.transform.position = new Vector3 (hit.point.x, hit.point.y, _wallPreviewRaycastOrigin.transform.position.z);
                    _wallPreview.transform.rotation = new Quaternion(Quaternion.identity.x, instantiateRotation.y, Quaternion.identity.z, instantiateRotation.w);
                }

                if(Mathf.Abs(Input.GetAxis(Constants.Input.UpAndDownAxis)) < 0.3f)
                {
                  //   _wallPreview.GetComponent<MeshRenderer>().materials.
                }

            }
        }

        private void InstantiateWall()
        {
            if (entity.isOwner)
            {
                var wallPrefab = BoltNetwork.Instantiate(_wallAbilityPrefab, _wallPreview.transform.position, _wallPreview.transform.rotation);

                _enableWallPreview = false;
                _wallPreviewRaycastOrigin.transform.localPosition = Vector3.zero;
                _wallPreview.SetActive(false);
            }
        }
    }
}
