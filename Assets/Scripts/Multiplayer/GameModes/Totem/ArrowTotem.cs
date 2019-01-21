using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    public class ArrowTotem : EntityBehaviour
    {
        [SerializeField] private TotemPossession _totemPossession;

        private Transform _totemPosition;
        private Renderer _arrowTotem;
        
        // Start is called before the first frame update
        void Start()
        {
            _totemPosition = TotemHelpers.GetTotemEntity().transform;
            _arrowTotem = GetComponent<Renderer>();
        }

        public override void Attached()
        {
            gameObject.SetActive(entity.isOwner);
        }

        // Update is called once per frame
        void Update()
        {
            // Inverted lookAt
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