using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace GameModes.Totem
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

        // PUBLIC

        public void FullyDischarge()
        {
            if (entity.isAttached && entity.isOwner)
            {
                state.CanPickTotem = false;
            }

            _chargingRoutine = StartCoroutine(RechargeEnergy());
            if(OnFullyDischarged != null) OnFullyDischarged.Invoke();
        }

        public void FullyDischargeForXSeconds(float seconds)
        {
            StartCoroutine(FullyDischargeForXSecondsRoutine(seconds));
        }

        public void FullyCharge()
        {
            if (entity.isAttached && entity.isOwner)
            {
                state.CanPickTotem = true;
            }
            if (OnFullyCharged != null) OnFullyCharged.Invoke();
        }

        // PRIVATE

        private IEnumerator RechargeEnergy()
        {
            yield return new WaitForSeconds(_secondsDischarged.Value);
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
