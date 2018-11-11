using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class TPBackBehaviour : MonoBehaviour
    {
        [Header("Owner")]
        public Transform _kart;
        public Quaternion _kartRotation;

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
            if (_kart != null)
            {
                CheckForMaxDistance();
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
            if (Vector3.Distance(_kart.position, transform.position) > MaxDistance)
            {
                StopCoroutine(AliveDuration());
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
    }
}
