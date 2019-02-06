using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloakPortalTransferBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _portalEffect;
    [SerializeField] GameObject _startPos;
    [SerializeField] GameObject _camPos;

    public void StartLerping(float duration)
    {
        StartCoroutine(LerpingCoroutine(duration));
    }

    IEnumerator LerpingCoroutine(float duration)
    {
        _portalEffect.SetActive(true);
        var _currentTimer = 0f;

        while (_currentTimer < duration)
        {
            _portalEffect.transform.position = Vector3.Lerp(_startPos.transform.position,
                _camPos.transform.position,
                _currentTimer / duration);

            _portalEffect.transform.localScale = Vector3.Lerp(new Vector3(0.1f, 0.1f, 0.1f),
                new Vector3(1f, 1f, 1f),
                _currentTimer / duration);

            _currentTimer += Time.deltaTime;
            yield return null;
        }
        _portalEffect.transform.position = _startPos.transform.position;
        _portalEffect.SetActive(false);
    }
}
