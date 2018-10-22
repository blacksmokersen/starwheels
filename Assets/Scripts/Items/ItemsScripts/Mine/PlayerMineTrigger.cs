using UnityEngine;
using Multiplayer;

namespace Items
{
    public class PlayerMineTrigger : MonoBehaviour
    {
        public Ownership Ownership;
        public bool Activated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.KartTrigger && Activated)
            {
                var otherPlayer = other.GetComponentInParent<PlayerSettings>();
                if (Ownership.IsNotSameTeam(otherPlayer) || Ownership.IsMe(otherPlayer))
                {
                    // Hit other player
                }
                GetComponentInParent<MineBehaviour>().PlayExplosion();
                GetComponentInParent<MineBehaviour>().DestroyObject(); // Destroy the mine root item
            }
        }
    }
}
