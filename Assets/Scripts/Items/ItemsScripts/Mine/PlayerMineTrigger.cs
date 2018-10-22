using UnityEngine;

namespace Items
{
    public class PlayerMineTrigger : MonoBehaviour
    {
        public bool Activated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.KartTrigger && Activated)
            {
                /*
                var owner = GetComponentInParent<PhotonView>().Owner;
                var target = other.gameObject.GetComponentInParent<PhotonView>().Owner;
                if (owner.GetTeam() != target.GetTeam() || owner == target)
                {
                    other.gameObject.GetComponentInParent<Kart.KartEvents>().CallRPC("OnHit");
                }
                */
                GetComponentInParent<MineBehaviour>().PlayExplosion();
                GetComponentInParent<MineBehaviour>().DestroyObject(); // Destroy the mine root item
            }
        }
    }
}
