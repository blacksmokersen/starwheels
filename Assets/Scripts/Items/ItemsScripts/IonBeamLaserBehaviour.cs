using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonBeamLaserBehaviour : MonoBehaviour
{

    public float Speed;
    public float SpeedOffset;
    public float GrowingWarningSpeed;
    public float MaxWarningScale;
    public bool onExplode;
    private Vector2 offset;
    public GameObject EffectiveAOE;
    public GameObject WarningPosition;

    private void Awake()
    {
        onExplode = true;
        float currentTimer = WarningPosition.transform.localScale.x;
    }

    private void Update()
    {
        offset = new Vector2(0, Time.deltaTime * SpeedOffset);
        EffectiveAOE.GetComponent<Renderer>().material.mainTextureOffset += offset;



        if (WarningPosition.transform.localScale.x >= MaxWarningScale)
        {
            if (WarningPosition.transform.localScale.x >= MaxWarningScale/2)
            {
                GrowingAoeWarning(GrowingWarningSpeed/2);
            }
            else
            {
                GrowingAoeWarning(GrowingWarningSpeed);
            }
        }
        else
        {
            if(onExplode)
            Explosion();
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
            StartCoroutine("ParticuleEffect");
            onExplode = false;
        }
    }

    IEnumerator ParticuleEffect()
    {
        GetComponent<ParticleSystem>().Emit(5000);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
