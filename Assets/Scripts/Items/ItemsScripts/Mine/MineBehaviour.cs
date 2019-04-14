using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.PhysicsUtils;

namespace Items
{
    public class MineBehaviour : NetworkDestroyable
    {
        [Header("Settings")]
        public float ActivationTime;
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;
        public float LivingTime;

        [Header("Sounds")]
        public AudioSource LaunchSource;
        public AudioSource IdleSource;
        public AudioSource ExplosionSource;

        [Header("Animation")]
        [SerializeField] private Animator _animator;

        [Header("Triggers To Activate")]
        [SerializeField] private List<Collider> _triggers;

        // CORE

        private void Start()
        {
            StartCoroutine(MineActivationDelay());
            StartCoroutine(MineStopRoutine());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                GetComponent<Hovering>().Disable();
                StopMine();
            }
        }

        // BOLT

        public override void Attached()
        {
            DestroyObject(LivingTime);
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
            SWExtensions.AudioExtensions.PlayClipObjectAndDestroy(ExplosionSource);
        }
        #endregion

        // PRIVATE

        private IEnumerator MineActivationDelay()
        {
            yield return new WaitForSeconds(ActivationTime);

            foreach (var trigger in _triggers)
            {
                trigger.enabled = true;
            }
        }

        private void StopMine()
        {
            var rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            rb.freezeRotation = true;
            _animator.SetTrigger("Stop");
            PlayIdleSound();
        }

        private IEnumerator MineStopRoutine()
        {
            yield return new WaitForSeconds(1f);
            GetComponent<Hovering>().Disable();
        }
    }
}
