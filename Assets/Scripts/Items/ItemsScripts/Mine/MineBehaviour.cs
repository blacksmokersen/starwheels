using System.Collections;
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

        // CORE

        private void Awake()
        {
            _ownerShip = GetComponent<Ownership>();
        }

        private void Start()
        {

            StartCoroutine(MineActivationDelay());
            GetComponentInChildren<PlayerMineTrigger>().Ownership = _ownerShip;
            GetComponentInChildren<ItemMineTrigger>().Ownership = _ownerShip;
        }



        IEnumerator MineActivationDelay()
        {
            yield return new WaitForSeconds(ActivationTime);


            GetComponentInChildren<PlayerMineTrigger>().Activated = true;
            GetComponentInChildren<ItemMineTrigger>().Activated = true;
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

        //PUBLIC

        public void Update()
        {
            DestroyObject(LivingTime);
        }
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
    }
}
