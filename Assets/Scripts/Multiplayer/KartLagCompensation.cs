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
            if (BoltNetwork.IsServer && evnt.TargetBoltEntity == GetComponentInParent<BoltEntity>())
            {
                ServerCollisionCheck(evnt.ItemCollisionPosition,evnt.ItemBoltEntity, evnt.TargetBoltEntity, evnt.FrameToRewindTo);
            }
        }

        public void ServerCollisionCheck(Vector3 localCollisionPosition,BoltEntity item, BoltEntity target, int targetedFrame)
        {
            //  int FramesToRewind = BoltNetwork.Frame - targetedFrame;
            if (ServerRewindDistCheck(localCollisionPosition, target, NumberOfFramesToRewind) <= CollisionDistance)
            {
                Debug.LogError("HIT WITH LAG COMPENSATION -- FrameToRewind : " + NumberOfFramesToRewind +
                    " -- DISTANCE : " + ServerRewindDistCheck(localCollisionPosition, target, NumberOfFramesToRewind));


                var itemCollider = item.gameObject.GetComponentInChildren<ItemCollisionTrigger>().gameObject.GetComponent<SphereCollider>();

                target.gameObject.GetComponentInChildren<KartCollisionTrigger>().CheckTargetInformationsBeforeSendingHitEvent(itemCollider);


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
