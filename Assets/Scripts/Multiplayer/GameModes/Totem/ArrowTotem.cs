using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    public class ArrowTotem : EntityBehaviour
    {
        [SerializeField] private TotemPossession _totemPossession;
        [SerializeField] private Renderer _arrowTotem;

        private Transform _totemPosition;

        void Start()
        {

        }

        public override void Attached()
        {
            gameObject.SetActive(entity.isOwner);
        }

        void Update()
        {
            if (!_totemPosition)
            {
                _totemPosition = TotemHelpers.GetTotemEntity().transform;
            }
            else
            {
                //this.transform.LookAt(2 * transform.position - _totemPosition.position);

                //Arrow stick to the ground
                this.transform.forward = new Vector3(-(_totemPosition.transform.position.x - this.transform.position.x), 0, -(_totemPosition.transform.position.z - this.transform.position.z));

                if (_totemPossession._isLocalOwner)
                {
                    _arrowTotem.enabled = false;
                }
                else
                {
                    _arrowTotem.enabled = true;
                }
            }
        }
    }
}
