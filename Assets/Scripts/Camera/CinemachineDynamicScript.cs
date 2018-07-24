using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineDynamicScript : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    private Coroutine cameraBoostCoroutine;
    private CinemachineTransposer transposer;

    private float currentTimer;

    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        transposer = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void BoostCameraBehaviour()
    {
        if (cameraBoostCoroutine == null)
            cameraBoostCoroutine = StartCoroutine(CameraBoostBehaviour(-8.5f, 1f));
        currentTimer = 0f;
    }

    IEnumerator CameraBoostBehaviour(float endValue, float boostDuration)
    {
        float startValue = transposer.m_FollowOffset.z;
        float effectDuration = boostDuration / 2f;

        currentTimer = 0f;
        while (currentTimer < effectDuration)
        {
            transposer.m_FollowOffset.z = Mathf.Lerp(startValue, endValue, currentTimer / effectDuration);

            currentTimer += Time.deltaTime;
            yield return null;
        }

        currentTimer = 0f;
        while (currentTimer < effectDuration)
        {
            transposer.m_FollowOffset.z = Mathf.Lerp(endValue, startValue, currentTimer / effectDuration);
            
            currentTimer += Time.deltaTime;
            yield return null;
        }
    }
}
