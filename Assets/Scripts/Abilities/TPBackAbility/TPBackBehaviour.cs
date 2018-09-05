using Items;
using System.Collections;
using UnityEngine;

namespace Abilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class TPBackBehaviour : MonoBehaviour
    {
        [Header("TPBack parameters")]
        public float ActivationTime;
        public float AliveTime;
        public float MaxDistance;
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;

        [Header("Sounds")]
        public AudioClip LaunchSound;

        private AudioSource _audioSource;
        private bool _canBeEnabled = false;
        private bool _enabled = false;
        private Transform _kart;

        // CORE

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(ActivationDelay());
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

        public void Launch(KartInventory kart, Direction direction)
        {
            _kart = kart.GetComponentInParent<Rigidbody>().transform;

            if (direction == Direction.Forward)
            {
                transform.position = kart.ItemPositions.FrontPosition.position;
                GetComponent<Rigidbody>().AddForce((kart.transform.forward + kart.transform.up / TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
            else if (direction == Direction.Backward)
            {
                transform.position = kart.ItemPositions.BackPosition.position;
            }

            PlayLaunchSound();
        }

        public void PlayLaunchSound()
        {
            _audioSource.PlayOneShot(LaunchSound);
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
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
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
