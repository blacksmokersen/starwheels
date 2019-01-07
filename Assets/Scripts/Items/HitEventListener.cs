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

        // BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            var kartEntity = GetComponentInParent<BoltEntity>();
            if (kartEntity == evnt.VictimEntity)
            {
                if(OnPlayerHit != null) OnPlayerHit.Invoke();
            }
        }

        public override void OnEvent(DestroyEntity evnt)
        {
            if (evnt.Entity != null
                && evnt.Entity.isOwner)
            {
                BoltNetwork.Destroy(evnt.Entity);
            }
        }
    }
}
