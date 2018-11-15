using UnityEngine;
using Multiplayer;

namespace Items
{
    public class PlayerMineTrigger : MonoBehaviour
    {
        public Ownership Ownership;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.CollisionHitBox)
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (Ownership.IsNotSameTeam(otherPlayer) || Ownership.IsMe(otherPlayer))
                {
                    PlayerHit playerHitEvent = PlayerHit.Create();
                    playerHitEvent.PlayerEntity = other.GetComponentInParent<BoltEntity>();
                    playerHitEvent.Send();
                }

                GetComponentInParent<MineBehaviour>().PlayExplosion();
                GetComponentInParent<MineBehaviour>().DestroyObject(); // Destroy the mine root item
            }
        }
    }
}
