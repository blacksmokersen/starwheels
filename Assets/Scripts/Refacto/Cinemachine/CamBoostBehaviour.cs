using System.Collections;
using UnityEngine;
using Cinemachine;

public class CamBoostBehaviour : MonoBehaviour
{
    [SerializeField] private CamBoostSettings _camBoostSettings;

    private Coroutine _cameraBoostCoroutine;
    private CinemachineTransposer _transposer;
    private CinemachineVirtualCamera _cinemachine;
    private float _currentTimer;

    private void Awake()
    {
        _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
        _transposer = _cinemachine.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void BoostCameraBehaviour()
    {
        if (_cameraBoostCoroutine != null)
            StopCoroutine(_cameraBoostCoroutine);
        _cameraBoostCoroutine = StartCoroutine(CameraBoostBehaviour(_camBoostSettings.StartingDistanceCamInBoost, _camBoostSettings.MaxDistanceCamInBoost, _camBoostSettings.BoostDuration));
        _currentTimer = 0f;
    }

    IEnumerator CameraBoostBehaviour(float startValue, float endValue, float boostDuration)
    {
        float startDynamicCamValue = _transposer.m_FollowOffset.z;

        _currentTimer = 0f;
        while (_currentTimer < boostDuration)
        {
            //   if (!backCamActivated)
            //   {
            _transposer.m_FollowOffset.z = Mathf.Lerp(startDynamicCamValue, endValue, _currentTimer / boostDuration);
            _currentTimer += Time.deltaTime;
            yield return null;
            //  }
            //   else
            //      break;
        }
        yield return new WaitForSeconds(1f);

        _currentTimer = 0f;
        while (_currentTimer < (boostDuration * _camBoostSettings.CamBoostReturnOnKartDelay))
        {
            //  if (!_backCamActivated)
            //  {
            _transposer.m_FollowOffset.z = Mathf.Lerp(endValue, startValue, _currentTimer / (boostDuration * _camBoostSettings.CamBoostReturnOnKartDelay));
            _currentTimer += Time.deltaTime;
            yield return null;
            //  }
            //  else
            //      break;
        }
    }
}
