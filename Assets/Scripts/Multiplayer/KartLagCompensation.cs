using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Items
{
    public class KartLagCompensation : GlobalEventListener
    {
        public int CollisionDistance;
        public int NumberOfFramesToRewind;

        public Collider KartCollider;
        public KartCollisionTrigger KartcollisionTrigger;

        public override void OnEvent(ItemsLagCheckEvent evnt)
        {
            if (BoltNetwork.IsServer && evnt.BoltEntity == GetComponentInParent<BoltEntity>())
            {
                ServerCollisionCheck(evnt.ItemCollisionPosition, evnt.BoltEntity, evnt.FrameToRewindTo);
            }
        }

        public void ServerCollisionCheck(Vector3 localCollisionPosition, BoltEntity Target, int targetedFrame)
        {
            //  int FramesToRewind = BoltNetwork.Frame - targetedFrame;
            if (ServerRewindDistCheck(localCollisionPosition, Target, NumberOfFramesToRewind) <= CollisionDistance)
            {
                Debug.LogError("HIT WITH LAG COMPENSATION -- FrameToRewind : " + NumberOfFramesToRewind +
                    " -- DISTANCE : " + ServerRewindDistCheck(localCollisionPosition, Target, NumberOfFramesToRewind));

                Debug.Log(Target.gameObject.GetComponentInChildren<KartLagCompensation>().KartCollider);


                Target.gameObject.GetComponentInChildren<KartCollisionTrigger>().CheckTargetInformationsBeforeSendingHitEvent(Target.gameObject.GetComponentInChildren<KartLagCompensation>().KartCollider);


              //  KartcollisionTrigger.CheckTargetInformationsBeforeSendingHitEvent(Target.gameObject.GetComponentInChildren<KartLagCompensation>().KartCollider);
            }
        }

        float ServerRewindDistCheck(Vector3 localCollisionPosition, BoltEntity target, int framesToRewind)
        {

            var targetHitbox = target.GetComponent<BoltHitboxBody>();
            Vector3 targetRewindPos = BoltNetwork.PositionAtFrame(targetHitbox, BoltNetwork.Frame - framesToRewind);
            return Vector3.Distance(localCollisionPosition, targetRewindPos);
        }
    }
}
