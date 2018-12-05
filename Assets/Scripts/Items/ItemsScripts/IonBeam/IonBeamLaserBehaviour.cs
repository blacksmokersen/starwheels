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

        private ParticleSystem _explosionParticleSystem;
        private Vector2 offset;
        private bool _onExplode = false;
        private bool _damagePlayer = false;
        private Coroutine _laserBehaviour;

        //CORE

        private void Awake()
        {
            _explosionParticleSystem = explosionParticles.GetComponent<ParticleSystem>();
            _onExplode = true;
            // GameObject owner = GetComponent<Ownership>().gameObject;
            //float currentTimer = WarningPosition.transform.localScale.x;
        }

        private void Start()
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastTransformOrigin.position, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
            {
                effectiveAOE.transform.position = hit.point;
                warningPosition.transform.position = hit.point;
            }
        }

        private void Update()
        {
            offset = new Vector2(0, Time.deltaTime * ionBeamLaserSettings.SpeedOffset);
          //  if (effectiveAOE != null)
              //  effectiveAOE.GetComponent<Renderer>().material.mainTextureOffset += offset;

            if (warningPosition != null)
            {
                if (warningPosition.transform.localScale.x >= ionBeamLaserSettings.MaxWarningScale)
                {
                    if (warningPosition.transform.localScale.x >= ionBeamLaserSettings.MaxWarningScale / 10)
                    {
                        GrowingAoeWarning(ionBeamLaserSettings.GrowingWarningSpeed / 10);
                    }
                    else
                    {
                        GrowingAoeWarning(ionBeamLaserSettings.GrowingWarningSpeed);
                    }
                }
                else
                {
                    if (_onExplode)
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
            if (_onExplode)
            {
                RaycastHit hit;
                if (Physics.Raycast(raycastTransformOrigin.position, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
                {
                    explosionParticles.transform.position = hit.point;
                    Destroy(effectiveAOE);
                    Destroy(warningPosition);
                    _laserBehaviour = StartCoroutine(ParticuleEffect());
                    _onExplode = false;
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
            // MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(ExplosionSource);
            explosionParticles.SetActive(true);
            _damagePlayer = true;
            yield return new WaitForSeconds(0.2f);
            _damagePlayer = false;
            /*
            var shape = _explosionParticleSystem.shape;
            shape.radius = 10;
            _explosionParticleSystem.Emit(500);
            _damagePlayer = true;
            yield return new WaitForSeconds(0.1f);
            shape.radius = 20;
            _explosionParticleSystem.Emit(500);
            yield return new WaitForSeconds(0.1f);
            shape.radius = 30;
            _explosionParticleSystem.Emit(500);
            _damagePlayer = false;
            */
            yield return new WaitForSeconds(2);
            BoltNetwork.Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (_damagePlayer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.KartHealthHitBox))
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
