using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineDynamicScript : BaseKartComponent
{
    [Range(7.5f, 15f)] public float MaxDistanceCamInBoost;

    private CinemachineVirtualCamera cinemachine;
    private Coroutine cameraBoostCoroutine;
    private CinemachineTransposer transposer;

    private float currentTimer;

    private new void Awake()
    {
        base.Awake();
        kartEvents.OnDriftBoost += BoostCameraBehaviour;

        cinemachine = GetComponent<CinemachineVirtualCamera>();
        transposer = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void BoostCameraBehaviour()
    {
        if (cameraBoostCoroutine != null)
            StopCoroutine(cameraBoostCoroutine);
        cameraBoostCoroutine = StartCoroutine(CameraBoostBehaviour(-7.5f, -MaxDistanceCamInBoost, 1f));
        currentTimer = 0f;
    }

    IEnumerator CameraBoostBehaviour(float startValue, float endValue, float boostDuration)
    {
        float startDynamicCamValue = transposer.m_FollowOffset.z;
        float effectDuration = boostDuration / 2f;

        currentTimer = 0f;
        while (currentTimer < effectDuration)
        {
            transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValue, endValue, currentTimer / effectDuration);
            currentTimer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        currentTimer = 0f;
        while (currentTimer < effectDuration)
        {
            transposer.m_FollowOffset.z = Mathf.Lerp(endValue, startValue, currentTimer / effectDuration);
            currentTimer += Time.deltaTime;
            yield return null;
        }
    }
}
