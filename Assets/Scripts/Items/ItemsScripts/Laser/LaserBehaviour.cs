using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine;

namespace Items
{
    public class LaserBehaviour : NetworkDestroyable
    {
        [SerializeField] private LaserSettings _laserSettings;
        //CORE

        private void Start()
        {
            StartCoroutine(LaserDuration(_laserSettings.LaserDuration));
        }

        //PUBLIC

         public void LaunchLaser(Transform kartPos, Rigidbody kartRigidbody)
        {
            Transform LaserPosition = kartPos;
            transform.position = kartPos.position;
            transform.rotation = new Quaternion(Quaternion.identity.x, kartPos.rotation.y, Quaternion.identity.z, kartPos.rotation.w);
            RecoilImpulseToKart(kartRigidbody);
        }

        //PRIVATE

        private void RecoilImpulseToKart(Rigidbody kartRigidbody)
        {
            kartRigidbody.AddRelativeForce(Vector3.forward * -_laserSettings.KartRecoilPower, ForceMode.Impulse);
        }

        private IEnumerator LaserDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
            destroyEntityEvent.Entity = entity;
            destroyEntityEvent.Send();
        }
    }
}
