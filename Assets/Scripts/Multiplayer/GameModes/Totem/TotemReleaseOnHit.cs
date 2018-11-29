using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    public class TotemReleaseOnHit : EntityBehaviour<IKartState>
    {
        [SerializeField] private Health.Health _health;

        private void Start()
        {
            _health.OnHealthLoss.AddListener((i) => ReleaseTotem());
        }

        private void ReleaseTotem()
        {
            var totem = FindObjectOfType<TotemBehaviour>();

            if (totem.GetComponent<BoltEntity>().GetState<IItemState>().OwnerID == state.OwnerID) // The owner of the totem was hit
            {
                totem.UnsetParent();
            }
        }
    }
}
