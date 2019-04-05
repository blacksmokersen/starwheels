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


        private Transform _previewPosition;

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner && gameObject.activeInHierarchy)
            {
                MapInputs();
            }
        }

        public void MapInputs()
        {
            if (Enabled && Input.GetButtonDown(Constants.Input.UseAbility))
            {

            }
            if (Enabled && Input.GetButtonUp(Constants.Input.UseAbility))
            {
                InstantiateWall();
            }



        }



        private void JumpToTopOfThePrefab()
        {

        }

        private void InstantiateWall()
        {
            if (entity.isOwner)
            {
                Vector3 instantiatePosition = _previewPosition.position;

                var wallPrefab = BoltNetwork.Instantiate(_wallAbilityPrefab, instantiatePosition, Quaternion.identity);


            }
        }
    }
}
