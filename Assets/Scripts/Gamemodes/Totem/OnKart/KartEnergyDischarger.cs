using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Gamemodes.Totem
{
    public class KartEnergyDischarger : EntityBehaviour<IKartState>
    {
        [Header("Settings")]
        [SerializeField] private FloatVariable _secondsDischarged;

        [Header("Events")]
        public UnityEvent OnFullyCharged;
        public UnityEvent OnFullyDischarged;

        private Coroutine _chargingRoutine;

        // BOLT

        public override void Attached()
        {
            if (entity.isOwner && entity.isAttached)
            {
                state.CanPickTotem = true;
            }
        }

        public override void ControlGained()
        {
            TotemChargeHUD totemChargeHUD = FindObjectOfType<TotemChargeHUD>();
            if (totemChargeHUD)
            {
                totemChargeHUD.Observe(gameObject);
            }
            else
            {
                Debug.Log("TotemChargeHUD HUD not found (maybe gamemode is not totem).");
            }
        }

        // PUBLIC

        public void FullyDischarge()
        {
            if (entity.isAttached && entity.isOwner)
            {
                state.CanPickTotem = false;
            }
            if(OnFullyDischarged != null) OnFullyDischarged.Invoke();
        }

        public void FullyDischargeForXSeconds(FloatVariable seconds)
        {
            StartCoroutine(FullyDischargeForXSecondsRoutine(seconds.Value));
        }

        public void FullyCharge()
        {
            if (entity.isAttached && entity.isOwner)
            {
                state.CanPickTotem = true;
            }
            if (OnFullyCharged != null) OnFullyCharged.Invoke();
        }

        public void FullyChargeAfterXSeconds(FloatVariable seconds)
        {
            StartCoroutine(RechargeEnergyAfterXSeconds(seconds.Value));
        }

        // PRIVATE

        private IEnumerator RechargeEnergyAfterXSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            FullyCharge();
        }

        private IEnumerator FullyDischargeForXSecondsRoutine(float seconds)
        {
            FullyDischarge();
            yield return new WaitForSeconds(seconds);
            FullyCharge();
        }
    }
}
