using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class MineBehaviour : NetworkDestroyable
    {
        [Header("Mine parameters")]
        public float ActivationTime;
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;
        public float LivingTime;

        [Header("Sounds")]
        public AudioSource LaunchSource;
        public AudioSource IdleSource;
        public AudioSource ExplosionSource;

        private Ownership _ownerShip;
        private List<Collider> _triggers = new List<Collider>();

        // CORE

        private void Awake()
        {
            _ownerShip = GetComponent<Ownership>();       
            foreach(var col in GetComponentsInChildren<Collider>())
            {
                if (col.isTrigger)
                {
                    _triggers.Add(col);
                    col.enabled = false;
                }
            }
        }

        private void Start()
        {
            StartCoroutine(MineActivationDelay());
        }

        // BOLT

        public override void ControlGained()
        {
            if (entity.isOwner)
            {
                BoltEntity.Destroy(entity, 10f);
            }
        }

        // PUBLIC

        #region Audio
        public void PlayLaunchSound()
        {
            LaunchSource.Play();
        }

        public void PlayIdleSound()
        {
            IdleSource.loop = true;
            IdleSource.Play();
        }

        public void PlayExplosion()
        {
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(ExplosionSource);
        }
        #endregion

        // PRIVATE

        private IEnumerator MineActivationDelay()
        {
            yield return new WaitForSeconds(ActivationTime);

            foreach (var trigger in _triggers)
                trigger.enabled = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                GetComponent<Rigidbody>().freezeRotation = true;
                PlayIdleSound();
            }
        }
    }
}
