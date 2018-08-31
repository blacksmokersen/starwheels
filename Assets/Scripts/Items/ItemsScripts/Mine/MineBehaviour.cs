using System.Collections;
using UnityEngine;

namespace Items
{
    public class MineBehaviour : ItemBehaviour
    {
        [Header("Mine parameters")]
        public float ActivationTime;
        public float ForwardThrowingForce;
        public float TimesLongerThanHighThrow;

        [Header("Sounds")]
        public AudioClip LaunchSound;
        public AudioClip IdleSound;
        public AudioClip ExplosionSound;

        private AudioSource audioSource;

        #region Behaviour
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(MineActivationDelay());
        }

        public override void Spawn(KartInventory kart, Direction direction)
        {
            if (direction == Direction.Forward)
            {
                transform.position = kart.ItemPositions.FrontPosition.position;
                GetComponent<Rigidbody>().AddForce((kart.transform.forward + kart.transform.up/TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
            else if (direction == Direction.Backward || direction == Direction.Default)
            {
                transform.position = kart.ItemPositions.BackPosition.position;
            }
            PlayLaunchSound();
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
                PlayIdleSound();
            }
        }

        #endregion

        #region Audio
        public void PlayLaunchSound()
        {
            audioSource.PlayOneShot(LaunchSound);
        }

        public void PlayIdleSound()
        {
            audioSource.clip = IdleSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void PlayExplosion()
        {
            AudioSource.PlayClipAtPoint(ExplosionSound, transform.position);
        }
        #endregion
    }
}
