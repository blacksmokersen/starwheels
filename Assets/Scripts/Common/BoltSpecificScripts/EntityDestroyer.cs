﻿using UnityEngine;
using Bolt;
using System.Collections;

namespace Common.Bolt
{
    public class EntityDestroyer : EntityBehaviour
    {
        [Header("Entity")]
        [SerializeField] private BoltEntity _targetEntity;

        // PUBLIC

        public void DestroyEntity()
        {
            if (entity.isOwner)
            {
                BoltNetwork.Destroy(_targetEntity);
            }
        }

        public void DestroyEntityAfterXSeconds(float x)
        {
            StartCoroutine(DestroyEntityAfterXSecondsRoutine(x));
        }

        // PRIVATE

        private IEnumerator DestroyEntityAfterXSecondsRoutine(float x)
        {
            yield return new WaitForSeconds(x);
            DestroyEntity();
        }
    }
}
