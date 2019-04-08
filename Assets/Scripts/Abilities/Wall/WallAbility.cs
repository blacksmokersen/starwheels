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

        [Header("WallSettings")]
        [SerializeField] private WallSettings _wallSettings;

        [Header("WallPrefab")]
        [SerializeField] private GameObject _wallAbilityPrefab;
        [SerializeField] private GameObject _wallPreviewRaycastOrigin;
        [SerializeField] private GameObject _wallPreview;
        [SerializeField] private GameObject _wallPreviewPerpendiculary;
        [SerializeField] private GameObject _wallPreviewInLine;

        private bool _enableWallPreview = false;
        private bool _previewIsEnable = false;
        private Rigidbody _rb;

        //CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
            _wallPreview = _wallPreviewPerpendiculary;
        }

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
            DebugSwitchLinePreview();
            if (CanUseAbility)
            {
                if (Enabled && Input.GetButtonDown(Constants.Input.UseAbility))
                {
                    _wallPreview.SetActive(true);
                    _enableWallPreview = true;
                    _previewIsEnable = true;
                }
                if (Enabled && Input.GetButtonUp(Constants.Input.UseAbility) && _previewIsEnable)
                {
                    InstantiateWall();
                    _previewIsEnable = false;
                }
            }
        }

        public new void Reload()
        {
            CanUseAbility = true;
        }

        //DEBUG

        private void DebugSwitchLinePreview()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                _wallPreview = _wallPreviewPerpendiculary;
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                _wallPreview = _wallPreviewInLine;
            }
        }

        //PRIVATE

        private void JumpToTopOfThePrefab()
        {
            _rb.AddRelativeForce(Vector3.up * _wallSettings.WallJumpValue, ForceMode.Impulse);
        }

        private void MovePreviewRaycast()
        {
            var leftJoytstickInput = Input.GetAxis(Constants.Input.UpAndDownAxis);
            _wallPreviewRaycastOrigin.transform.localPosition = Vector3.forward *
                (int)Mathf.Clamp((leftJoytstickInput * _wallSettings.WallMaxRange), _wallSettings.WallMinRange, _wallSettings.WallMaxRange);
        }

        private void PreviewWall()
        {
            if (entity.isOwner)
            {
                RaycastHit hit;
                if (Physics.Raycast(_wallPreviewRaycastOrigin.transform.position, Vector3.down, out hit, 100, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
                {
                    Quaternion instantiateRotation = _wallPreview.transform.rotation;
                    _wallPreview.transform.position = new Vector3(hit.point.x, hit.point.y, _wallPreviewRaycastOrigin.transform.position.z);
                    _wallPreview.transform.rotation = new Quaternion(Quaternion.identity.x, instantiateRotation.y, Quaternion.identity.z, instantiateRotation.w);
                }
            }
        }

        private void InstantiateWall()
        {
            if (entity.isOwner)
            {
                if (Mathf.Abs(Input.GetAxis(Constants.Input.UpAndDownAxis)) < 0.1f)
                {
                    JumpToTopOfThePrefab();
                }

                var wallPrefab = BoltNetwork.Instantiate(_wallAbilityPrefab, _wallPreview.transform.position, _wallPreview.transform.rotation);

                _enableWallPreview = false;
                _wallPreviewRaycastOrigin.transform.localPosition = Vector3.zero;
                _wallPreview.SetActive(false);
                StartCoroutine(Cooldown());
            }
        }
    }
}
