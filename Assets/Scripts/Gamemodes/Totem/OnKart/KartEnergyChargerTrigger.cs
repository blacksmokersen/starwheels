using UnityEngine;
using Bolt;

namespace Gamemodes.Totem
{
    [DisallowMultipleComponent]
    public class KartEnergyChargerTrigger : EntityBehaviour<IKartState>
    {
        [SerializeField] private KartEnergyDischarger _energyDischarger;

        private TotemOwnership _totemOwnership;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.ItemBox))
            {
                if (!_totemOwnership)
                {
                    _totemOwnership = FindObjectOfType<TotemOwnership>();
                }

                if (entity.isAttached && !state.CanPickTotem && !_totemOwnership.IsLocalOwner(state.OwnerID))
                {
                    Debug.Log("Recharching kart.");
                    _energyDischarger.FullyCharge();
                    other.GetComponent<Items.Lottery.ItemBox>().Activate();
                }
            }
        }
    }
}
