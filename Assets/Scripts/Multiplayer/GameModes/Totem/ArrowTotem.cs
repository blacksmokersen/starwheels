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
                this.transform.LookAt(2 * transform.position - _totemPosition.position);

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
