﻿using UnityEngine;
using Bolt;
using System.Collections;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class MineLerping : EntityBehaviour<IItemState>
    {
        [Header("Item Root")]
        [SerializeField] private GameObject _mineRoot;

        [Header("Animation")]
        [SerializeField] private Animator _animator;

        [Header("Settings")]
        [SerializeField] private FloatVariable _speedMultiplicator;

        [Header("Mesh to Hide")]
        [SerializeField] private MeshRenderer _shieldMesh;

        private float _speed = 0.05f;
        private bool _lerping = false;
        private GameObject _target;

        // CORE

        private void Update()
        {
            if (_lerping && _target)
            {
                _mineRoot.transform.position = Vector3.Lerp(transform.position, _target.transform.position, _speed);
                SpeedIncrease();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartHealthHitBox))
            {
                if (other.GetComponentInParent<BoltEntity>().GetState<IKartState>().Team != state.Team)
                {
                    StartLerpingTowardTarget(other.gameObject);
                }
            }
        }

        // PRIVATE

        private void StartLerpingTowardTarget(GameObject target)
        {
            _target = target;
            _animator.SetTrigger("Jump");
            _shieldMesh.enabled = false;

            StartCoroutine(WaitBeforeLerping());
        }

        private void SpeedIncrease()
        {
            _speed *= _speedMultiplicator.Value;
        }

        private IEnumerator WaitBeforeLerping()
        {
            yield return new WaitForSeconds(0.75f);
            _lerping = true;
        }
    }
}