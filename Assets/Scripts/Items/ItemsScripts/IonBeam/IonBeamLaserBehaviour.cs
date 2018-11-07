using System.Collections;
using UnityEngine;

namespace Items
{
    public class IonBeamLaserBehaviour : MonoBehaviour
    {
        //  [SerializeField] private ProjectileBehaviour projectileBehaviour;

        [SerializeField] private IonBeamLaserSettings ionBeamLaserSettings;
        [SerializeField] private GameObject effectiveAOE;
        [SerializeField] private GameObject warningPosition;
        [SerializeField] private GameObject explosionParticles;
        [SerializeField] private Transform raycastTransformOrigin;
        [SerializeField] private AudioSource explosionSource;

        private Vector2 offset;
        private bool _damagePlayer = false;
        private Coroutine _laserBehaviour;

        //CORE

        private void Awake()
        {
            ionBeamLaserSettings.onExplode = true;
            // GameObject owner = GetComponent<Ownership>().gameObject;
            //float currentTimer = WarningPosition.transform.localScale.x;
        }

        private void Update()
        {
            offset = new Vector2(0, Time.deltaTime * ionBeamLaserSettings.SpeedOffset);
            if (effectiveAOE != null)
                effectiveAOE.GetComponent<Renderer>().material.mainTextureOffset += offset;

            if (warningPosition != null)
            {
                if (warningPosition.transform.localScale.x >= ionBeamLaserSettings.MaxWarningScale)
                {
                    if (warningPosition.transform.localScale.x >= ionBeamLaserSettings.MaxWarningScale / 2)
                    {
                        GrowingAoeWarning(ionBeamLaserSettings.GrowingWarningSpeed / 2);
                    }
                    else
                    {
                        GrowingAoeWarning(ionBeamLaserSettings.GrowingWarningSpeed);
                    }
                }
                else
                {
                    if (ionBeamLaserSettings.onExplode)
                        Explosion();
                }
            }
        }

        // PUBLIC

        public void GrowingAoeWarning(float growSpeed)
        {
            float IncreaseSpeed = growSpeed * Time.deltaTime;
            warningPosition.transform.localScale += new Vector3(-IncreaseSpeed, 0, -IncreaseSpeed);
        }

        public void Explosion()
        {
            if (ionBeamLaserSettings.onExplode)
            {
                RaycastHit hit;
                if (Physics.Raycast(raycastTransformOrigin.position, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
                {
                    explosionParticles.transform.position = hit.point;
                    Destroy(effectiveAOE);
                    Destroy(warningPosition);
                    _laserBehaviour = StartCoroutine(ParticuleEffect());
                    ionBeamLaserSettings.onExplode = false;
                }
                else
                {
                    BoltNetwork.Destroy(gameObject);
                }
            }
        }

        //PRIVATE

        IEnumerator ParticuleEffect()
        {
            explosionParticles.GetComponent<ParticleSystem>().Emit(3000);
            // MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(ExplosionSource);
            _damagePlayer = true;
            yield return new WaitForSeconds(0.1f);
            _damagePlayer = false;
            yield return new WaitForSeconds(1);
            BoltNetwork.Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (_damagePlayer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.HealthHitBox))
                {
                    Debug.Log("Hit");
                    //  CheckCollision(other.gameObject);
                    PlayerHit playerHitEvent = PlayerHit.Create();
                    playerHitEvent.PlayerEntity = other.GetComponentInParent<BoltEntity>();
                    playerHitEvent.Send();
                }
            }
        }
    }
}
