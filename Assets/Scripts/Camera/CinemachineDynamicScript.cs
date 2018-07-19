using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineDynamicScript : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    private Coroutine cameraBoostCoroutine;
    private CinemachineTransposer transposer;

    private bool isrunning = false;

    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        transposer = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void BoostCameraBehaviour()
    {
        if (isrunning == false)
        {
            cameraBoostCoroutine = StartCoroutine(CameraBoostBehaviour(-7.5f, -8.5f, 1));
        }
    }

    IEnumerator CameraBoostBehaviour(float aValue, float bValue, float aTime)
    {
        isrunning = true;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            float cameraZ = Mathf.Lerp(aValue, bValue, t);
            transposer.m_FollowOffset.z = cameraZ;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (aTime*2))
        {
            float cameraZ = Mathf.Lerp(bValue, aValue, t);
            transposer.m_FollowOffset.z = cameraZ;
            yield return null;
        }
        isrunning = false;
    }
}
