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
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;

        [Header("Sounds")]
        public AudioClip LaunchSound;

        private AudioSource _audioSource;
        private bool _enabled = false;

        // CORE

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(ActivationDelay());
        }

        // PUBLIC

        public bool IsEnabled()
        {
            return _enabled;
        }

        public void Launch(KartInventory kart, Directions direction)
        {
            if (direction == Directions.Forward)
            {
                transform.position = kart.ItemPositions.FrontPosition.position;
                GetComponent<Rigidbody>().AddForce((kart.transform.forward + kart.transform.up / TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
            else if (direction == Directions.Backward)
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

        private IEnumerator ActivationDelay()
        {
            yield return new WaitForSeconds(ActivationTime);
            _enabled = true;
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
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
        }
    }
}
