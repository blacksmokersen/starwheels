using System.Collections;
using UnityEngine;
using Bolt;

namespace Items
{
    public class IonBeamLaserBehaviour : EntityBehaviour<IItemState>
    {

        [SerializeField] private GameObject _ionBeamCore;
        [SerializeField] private Collider _ionBeamCollider;
        [SerializeField] private GameObject _ionbeamLagCompensation;
        [SerializeField] private Transform raycastTransformOrigin;

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
               // _ionBeamCollider.enabled = true;
                _ionbeamLagCompensation.SetActive(true);
            }
            else
            {
                BoltNetwork.Destroy(gameObject);
            }
        }

        public void AtEndDamageAnimation()
        {
           // _ionBeamCollider.enabled = false;
            _ionbeamLagCompensation.SetActive(false);
        }

        public void AtEndAnimation()
        {
            BoltNetwork.Destroy(gameObject);
        }
    }
}
