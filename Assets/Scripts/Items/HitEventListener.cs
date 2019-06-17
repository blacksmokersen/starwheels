using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Health
{
    [RequireComponent(typeof(Health))]
    public class HitEventListener : GlobalEventListener
    {
        [Header("Events")]
        public UnityEvent OnPlayerHit;
        public UnityEvent IWasHit;

        // BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            var kartEntity = GetComponentInParent<BoltEntity>();
            if (kartEntity == evnt.VictimEntity)
            {
                if (OnPlayerHit != null)
                {
                    OnPlayerHit.Invoke();
                }

                if (kartEntity.IsOwner && IWasHit != null)
                {
                    IWasHit.Invoke();
                }
            }
        }

        public override void OnEvent(DestroyEntity evnt)
        {
            if (evnt.Entity != null
                && evnt.Entity.IsOwner)
            {
                BoltNetwork.Destroy(evnt.Entity);
            }
        }
    }
}
