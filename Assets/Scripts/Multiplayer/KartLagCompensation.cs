using UnityEngine;
using Bolt;

namespace Items
{
    public class KartLagCompensation : GlobalEventListener
    {
        public Collider KartCollider;
        public KartCollisionTrigger KartcollisionTrigger;

        private int framesToRewind = 0;

        //BOLT

        public override void OnEvent(ItemCollsionLagCompensationEvent evnt)
        {
            if (BoltNetwork.IsServer && evnt.TargetBoltEntity == GetComponentInParent<BoltEntity>())
            {
                try
                {
                    int ping = SWPing.GetPingAliasedForPlayer(evnt.ItemBoltEntity.GetComponent<Ownership>().OwnerID);
                    if (ping <= 1)
                        framesToRewind = 0;
                    else if (ping >= 2 && ping <= 20)
                        framesToRewind = 5;
                    else if (ping >= 21 && ping <= 45)
                        framesToRewind = 10;
                    else if (ping >= 46 && ping <= 75)
                        framesToRewind = 15;
                    else if (ping >= 76 && ping <= 125)
                        framesToRewind = 20;
                    else
                        framesToRewind = 25;

                    ServerCollisionCheck(evnt.ItemCollsionPosition, evnt.ItemBoltEntity, evnt.TargetBoltEntity, framesToRewind, evnt.CollisionDistanceCheck);
                }
                catch (System.NullReferenceException)
                {
                    Debug.LogError("Player Ownership not set - Number of frames to REWIND : 0 ");
                }
            }
        }

        //PUBLIC

        public void ServerCollisionCheck(Vector3 localCollisionPosition, BoltEntity item, BoltEntity target, int framestoRewind, float collisionDistance)
        {
            if (ServerRewindDistCheck(localCollisionPosition, target, framestoRewind) <= collisionDistance)
            {
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
