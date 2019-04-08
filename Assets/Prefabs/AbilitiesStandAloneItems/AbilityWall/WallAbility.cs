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

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner && gameObject.activeInHierarchy)
            {
                MapInputs();

                if (_enableWallPreview)
                {
                    PreviewWall();
                }
            }
        }

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

        private void JumpToTopOfThePrefab()
        {

        }


        private void PreviewWall()
        {
            if (entity.isOwner)
            {
                RaycastHit hit;
                if (Physics.Raycast(_wallPreviewRaycastOrigin.transform.position, Vector3.down, out hit, 100, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
                {
                    _wallPreview.transform.position = hit.point;

               //     _wallPreview.transform.rotation = new Quaternion(Quaternion.identity.x, instantiateRotation.y, Quaternion.identity.z, instantiateRotation.w);
                }
            }
        }

        private void InstantiateWall()
        {
            if (entity.isOwner)
            {
               // Vector3 instantiatePosition = _wallPreview.transform.position;
                Quaternion instantiateRotation = _wallPreview.transform.rotation;

                RaycastHit hit;
                if (Physics.Raycast(_wallPreviewRaycastOrigin.transform.position, Vector3.down, out hit, 100, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
                {
                    var wallPrefab = BoltNetwork.Instantiate(_wallAbilityPrefab, hit.point, new Quaternion(Quaternion.identity.x, instantiateRotation.y, Quaternion.identity.z, instantiateRotation.w));
                }


                _enableWallPreview = false;
                _wallPreview.transform.position = new Vector3(0,0,10);
                _wallPreview.SetActive(false);
            }
        }
    }
}
