using System.Collections;
using UnityEngine;

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

         public void LaunchLaser(GameObject kartRoot)
        {
            var kartTransform = kartRoot.transform;
            Transform LaserPosition = kartTransform;
            transform.position = kartTransform.position;
            transform.rotation = new Quaternion(Quaternion.identity.x, kartTransform.rotation.y, Quaternion.identity.z, kartTransform.rotation.w);

            var kartRb = kartRoot.GetComponentInChildren<Rigidbody>();
            RecoilImpulseToKart(kartRb);
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
