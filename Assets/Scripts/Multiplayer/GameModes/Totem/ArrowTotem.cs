using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    public class ArrowTotem : EntityBehaviour
    {
        [SerializeField] private TotemPossession _totemPossession;
        [SerializeField] private Renderer _arrowTotem;

        private Transform _totemPosition;

        // CORE

        private void Update()
        {
            if (entity.isOwner && entity.isAttached)
            {
                if (!_totemPosition)
                {
                    var totem = TotemHelpers.FindTotem();
                    if (totem)
                    {
                        _totemPosition = totem.transform;
                    }
                }
                else
                {
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

        // BOLT

        public override void Attached()
        {
            gameObject.SetActive(entity.isOwner);
        }
    }
}
