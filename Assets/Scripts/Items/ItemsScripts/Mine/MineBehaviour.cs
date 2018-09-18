﻿using System.Collections;
using UnityEngine;

namespace Items
{
    public class MineBehaviour : ItemBehaviour
    {
        [Header("Mine parameters")]
        public float ActivationTime;
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;

        [Header("Sounds")]
        public AudioSource LaunchSource;
        public AudioSource IdleSource;
        public AudioSource ExplosionSource;

        #region Behaviour

        private void Start()
        {
            StartCoroutine(MineActivationDelay());
        }

        public override void Spawn(KartInventory kart, Direction direction, float aimAxis)
        {
            if (direction == Direction.Forward)
            {
                transform.position = kart.ItemPositions.FrontPosition.position;
                var aimVector = kart.transform.forward + kart.transform.TransformDirection(new Vector3(aimAxis, 0, 0));
                GetComponent<Rigidbody>().AddForce((aimVector + kart.transform.up/TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
            else if (direction == Direction.Backward || direction == Direction.Default)
            {
                transform.position = kart.ItemPositions.BackPosition.position;
            }
            PlayLaunchSound();
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

        #endregion

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
            MyExtensions.Audio.PlayClipObjectAndDestroy(ExplosionSource);
        }
        #endregion
    }
}
