using System.Collections;
using UnityEngine;

namespace Items
{
    public class LaserBehaviour : NetworkDestroyable
    {
        [SerializeField] private LaserSettings _laserSettings;

        [SerializeField] private GameObject _laserFront;
        [SerializeField] private GameObject _laserBack;

        private LineRenderer _laserRenderer;
        private Collider _laserCollider;
        private MeshCollider _laserColliderBack;

        //CORE

        private void Start()
        {
            StartCoroutine(LaserDuration(_laserSettings.TimeBeforeDestroyLaser));
        }

        //PUBLIC

        public void LaunchMode(int mode)
        {
            switch (mode)
            {
                case 1:
                    _laserFront.SetActive(true);
                   // GetComponent<BoltHitboxBody>().proximity = _laserFront.GetComponentInChildren<BoltHitbox>();
                  //  GetComponent<BoltHitboxBody>().hitboxes[0] = _laserFront.GetComponentInChildren<BoltHitbox>();
                    _laserRenderer = _laserFront.GetComponentInChildren<LineRenderer>();
                    _laserCollider = _laserFront.GetComponentInChildren<CapsuleCollider>();
                    _laserBack.SetActive(false);
                    break;
                case 2:
                    _laserBack.SetActive(true);
                  //  GetComponent<BoltHitboxBody>().proximity = _laserBack.GetComponentInChildren<BoltHitbox>();
                 //   GetComponent<BoltHitboxBody>().hitboxes[0] = _laserBack.GetComponentInChildren<BoltHitbox>();
                    _laserRenderer = _laserBack.GetComponentInChildren<LineRenderer>();
                    _laserCollider = _laserBack.GetComponentInChildren<SphereCollider>();
                    _laserFront.SetActive(false);
                    break;
            }
        }

        public void LaunchLaser(GameObject kartRoot)
        {
            var kartTransform = kartRoot.transform;
         //   Transform LaserPosition = kartTransform;
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
            yield return new WaitForSeconds(_laserSettings.FlashDuration);
            if (_laserRenderer)
                _laserRenderer.enabled = false;
           // if (_laserColliderBack)
           //     _laserColliderBack.enabled = false;
            yield return new WaitForSeconds(_laserSettings.CollisionDuration);
            if (_laserCollider)
                _laserCollider.enabled = false;
            yield return new WaitForSeconds(duration);
            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
            destroyEntityEvent.Entity = entity;
            destroyEntityEvent.Send();
        }
    }
}
