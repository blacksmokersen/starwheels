using System.Collections;
using UnityEngine;

namespace Items{
    [RequireComponent(typeof(Rigidbody))]
    public class MineBehaviour : ItemBehaviour
    {
        [Header("Mine parameters")]
        public float ActivationTime;
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;

        private void Start()
        {
            StartCoroutine(MineActivationDelay());
        }

        public override void Spawn(KartInventory kart, Directions direction)
        {
            if (direction == Directions.Forward)
            {
                transform.position = kart.ItemPositions.FrontPosition.position;
                GetComponent<Rigidbody>().AddForce((kart.transform.forward + kart.transform.up/TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
            else if (direction == Directions.Backward || direction == Directions.Default)
            {
                transform.position = kart.ItemPositions.BackPosition.position;
            }
        }

        IEnumerator MineActivationDelay()
        {
            yield return new WaitForSeconds(ActivationTime);
            GetComponentInChildren<PlayerMineTrigger>().Activated = true;
            GetComponentInChildren<ItemMineTrigger>().Activated = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
        }
    }
}