using UnityEngine;
using Bolt;

namespace Gamemodes.Totem
{
    public class ArrowTotem : EntityBehaviour<IKartState>
    {
        [SerializeField] private Renderer _arrowTotem;

        private Transform _totemPosition;
        private TotemOwnership _totemOwnership;

        // CORE

        private void Update()
        {
            if (entity.isAttached && entity.isOwner)
            {
                if (!_totemPosition)
                {
                    var totem = TotemHelpers.FindTotem();
                    if (totem)
                    {
                        _totemPosition = totem.transform;
                        _totemOwnership = totem.GetComponent<TotemOwnership>();
                    }
                }
                else
                {
                    //Arrow stick to the ground
                    this.transform.forward = new Vector3(-(_totemPosition.transform.position.x - this.transform.position.x), 0, -(_totemPosition.transform.position.z - this.transform.position.z));

                    if (_totemOwnership.IsLocalOwner(state.OwnerID))
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
