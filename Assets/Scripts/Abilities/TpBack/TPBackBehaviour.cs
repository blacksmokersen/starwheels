using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class TPBackBehaviour : GlobalEventListener
    {
        [Header("Owner")]
        [HideInInspector] public Transform Kart;
        public Quaternion KartRotation;

        [Header("TPBack parameters")]
        public float ActivationTime;
        public float AliveTime;
        public float MaxDistance;
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;

        [Header("Events")]
        public UnityEvent OnTpBackLaunch;
        public UnityEvent OnTpBackFlight;
        public UnityEvent OnTpBackActivated;
        public UnityEvent OnTpBackIdle;

        [HideInInspector] public float ActualDistance;
        private bool _canBeEnabled = false;
        private bool _enabled = false;

        // CORE

        private void Start()
        {
            StartCoroutine(ActivationDelay());
            OnTpBackLaunch.Invoke();
            OnTpBackFlight.Invoke();
        }

        private void Update()
        {
            if (Kart != null)
            {
                CheckForMaxDistance();
            }
        }

        //BOLT

        public override void OnEvent(PlayerHit evnt)
        {
            if (GetComponent<BoltEntity>().IsOwner)
            {
                if (evnt.VictimID == Kart.GetComponent<Multiplayer.PlayerInfo>().OwnerID)
                {
                    Kart.GetComponentInChildren<Abilities.TPBackAbility>().OnDislocatorDestroyed.Invoke();
                    Destroy(gameObject);
                }
            }
        }

        // PUBLIC

        public bool IsEnabled()
        {
            return _enabled;
        }

        // PRIVATE

        private void CheckForMaxDistance()
        {
            ActualDistance = Vector3.Distance(Kart.position, transform.position);
            if (ActualDistance > MaxDistance)
            {
                StopCoroutine(AliveDuration());
                Kart.GetComponentInChildren<Abilities.TPBackAbility>().OnDislocatorDestroyed.Invoke();
                Destroy(gameObject);
            }
        }

        private IEnumerator ActivationDelay()
        {
            yield return new WaitForSeconds(ActivationTime);

            if (_canBeEnabled)
                _enabled = true;
            else
                _canBeEnabled = true;

            StartCoroutine(AliveDuration());
        }

        private IEnumerator AliveDuration()
        {
            yield return new WaitForSeconds(AliveTime);
            Kart.GetComponentInChildren<Abilities.TPBackAbility>().OnDislocatorDestroyed.Invoke();
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                OnTpBackActivated.Invoke();
                OnTpBackIdle.Invoke();
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                rb.freezeRotation = true;

                if (_canBeEnabled)
                    _enabled = true;
                else
                    _canBeEnabled = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {

        }
    }
}
