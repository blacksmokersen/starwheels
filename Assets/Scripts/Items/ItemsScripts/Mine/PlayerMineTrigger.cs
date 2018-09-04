using UnityEngine;

namespace Items
{
    public class PlayerMineTrigger : MonoBehaviour
    {
        public bool Activated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartTriggerTag && Activated)
            {
                var owner = GetComponentInParent<PhotonView>().owner;
                var target = other.gameObject.GetComponentInParent<PhotonView>().owner;
                if (owner.GetTeam() != target.GetTeam() || owner == target)
                {
                    other.gameObject.GetComponentInParent<Kart.KartEvents>().OnHit();
                }
                GetComponentInParent<MineBehaviour>().PlayExplosion();
                GetComponentInParent<MineBehaviour>().DestroyObject(); // Destroy the mine root item
            }
        }
    }
}
