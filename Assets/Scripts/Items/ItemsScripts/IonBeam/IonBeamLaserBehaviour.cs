using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kart;

public class IonBeamLaserBehaviour : MonoBehaviour
{

    public float Speed;
    public float SpeedOffset;
    public float GrowingWarningSpeed;
    public float MaxWarningScale;
    public bool onExplode;
    public bool DamagePlayer;
    private Vector2 offset;
    public GameObject EffectiveAOE;
    public GameObject WarningPosition;

    public AudioSource ExplosionSource;

    private void Awake()
    {
        onExplode = true;
        //float currentTimer = WarningPosition.transform.localScale.x;
    }

    private void Update()
    {
        offset = new Vector2(0, Time.deltaTime * SpeedOffset);
        if (EffectiveAOE != null)
            EffectiveAOE.GetComponent<Renderer>().material.mainTextureOffset += offset;


        if (WarningPosition != null)
        {
            if (WarningPosition.transform.localScale.x >= MaxWarningScale)
            {
                if (WarningPosition.transform.localScale.x >= MaxWarningScale / 2)
                {
                    GrowingAoeWarning(GrowingWarningSpeed / 2);
                }
                else
                {
                    GrowingAoeWarning(GrowingWarningSpeed);
                }
            }
            else
            {
                if (onExplode)
                    Explosion();
            }
        }
    }

    public void GrowingAoeWarning(float growSpeed)
    {
        float IncreaseSpeed = growSpeed * Time.deltaTime;
        WarningPosition.transform.localScale += new Vector3(-IncreaseSpeed, 0, -IncreaseSpeed);
    }

    public void Explosion()
    {
        if (onExplode)
        {
            Destroy(EffectiveAOE);
            Destroy(WarningPosition);
            StartCoroutine(ParticuleEffect());
            onExplode = false;
        }
    }

    IEnumerator ParticuleEffect()
    {
        GetComponent<ParticleSystem>().Emit(3000);
        MyExtensions.Audio.PlayClipObjectAndDestroy(ExplosionSource);
        DamagePlayer = true;
        yield return new WaitForSeconds(0.1f);
        DamagePlayer = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != null)
        {
            if(DamagePlayer)
                SendTargetOnHitEvent(other.gameObject);
        }
    }

    private void SendTargetOnHitEvent(GameObject kartCollision)
    {
        kartCollision.gameObject.gameObject.GetComponentInParent<KartEvents>().CallRPC("OnHit");
    }
}
