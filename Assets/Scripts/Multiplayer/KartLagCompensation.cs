using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace Items
{
    public class KartLagCompensation : GlobalEventListener
    {
        public Collider KartCollider;
        public KartCollisionTrigger KartcollisionTrigger;

        //BOLT

        public override void OnEvent(ItemCollsionLagCompensationEvent evnt)
        {
            if (BoltNetwork.IsServer && evnt.TargetBoltEntity == GetComponentInParent<BoltEntity>())
            {
                ServerCollisionCheck(evnt.ItemCollsionPosition, evnt.ItemBoltEntity, evnt.TargetBoltEntity, evnt.FramesToRewind, evnt.CollisionDistanceCheck);
            }
        }

        //PUBLIC

        public void ServerCollisionCheck(Vector3 localCollisionPosition, BoltEntity item, BoltEntity target, int framestoRewind, float collisionDistance)
        {
            if (ServerRewindDistCheck(localCollisionPosition, target, framestoRewind) <= collisionDistance)
            {

             //   Debug.LogError("HIT WITH LAG COMPENSATION -- FrameToRewind : " + framestoRewind +
             //       " -- DISTANCE : " + ServerRewindDistCheck(localCollisionPosition, target, framestoRewind));

                bool alreadySentInformations = false;

                foreach (Transform child in item.transform)
                {
                    if (!alreadySentInformations)
                    {
                        if (child.CompareTag(Constants.Tag.ItemCollisionHitBox))
                        {
                            var itemCollider = child.GetComponent<Collider>();
                            target.GetComponentInChildren<KartCollisionTrigger>().CheckTargetInformationsBeforeSendingHitEvent(itemCollider);
                            alreadySentInformations = true;
                        }
                        else
                        {
                            foreach (Transform secondChild in child.transform)
                            {
                                if (secondChild.CompareTag(Constants.Tag.ItemCollisionHitBox))
                                {
                                    var itemCollider = secondChild.GetComponent<Collider>();
                                    target.GetComponentInChildren<KartCollisionTrigger>().CheckTargetInformationsBeforeSendingHitEvent(itemCollider);
                                    alreadySentInformations = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        //PRIVATE

        private float ServerRewindDistCheck(Vector3 localCollisionPosition, BoltEntity target, int framesToRewind)
        {
            var targetHitbox = target.GetComponent<BoltHitboxBody>();
            Vector3 targetRewindPos = BoltNetwork.PositionAtFrame(targetHitbox, BoltNetwork.Frame - framesToRewind);
            return Vector3.Distance(localCollisionPosition, targetRewindPos);
        }
    }
}
