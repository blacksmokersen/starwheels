using System.Collections;
using UnityEngine;

namespace Items
{
    public class IonBeamLaserBehaviour : MonoBehaviour
    {
      //  [SerializeField] private ProjectileBehaviour projectileBehaviour;
      //  [SerializeField] private IonBeamLaserSettings _ionBeamLaserSettings;

        [SerializeField] private GameObject _ionBeamCore;
        [SerializeField] private Transform raycastTransformOrigin;

        private bool _damagePlayer = false;

        //CORE

        private void Start()
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastTransformOrigin.position, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
            {
                _ionBeamCore.transform.position = hit.point;
            }
        }

        //PUBLIC

        public void AtLaunchAnimation()
        {

        }

        public void AtDamageAnimation()
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastTransformOrigin.position, Vector3.down, out hit, 500, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
            {
                _damagePlayer = true;
            }
            else
            {
                BoltNetwork.Destroy(gameObject);
            }
        }

        public void AtEndDamageAnimation()
        {
            _damagePlayer = false;
        }

        public void AtEndAnimation()
        {
            BoltNetwork.Destroy(gameObject);
        }

        //PRIVATE

        private void OnTriggerStay(Collider other)
        {
            if (_damagePlayer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.KartHealthHitBox))
                {
                    Debug.Log("Hit");
                    //  CheckCollision(other.gameObject);
                    PlayerHit playerHitEvent = PlayerHit.Create();
                    playerHitEvent.PlayerEntity = other.GetComponentInParent<BoltEntity>();
                    playerHitEvent.Send();
                }
            }
        }
    }
}
