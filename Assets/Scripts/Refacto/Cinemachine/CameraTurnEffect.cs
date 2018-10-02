using UnityEngine;
using Cinemachine;

public class CameraTurnEffect : MonoBehaviour, IControllable
{
    private CinemachineOrbitalTransposer _orbiter;
    private CinemachineVirtualCamera _cinemachine;
    private bool _backCamActivated = false;

    private void Awake()
    {
        _cinemachine = GetComponentInParent<CinemachineVirtualCamera>();
        _orbiter = _cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    private void Start()
    {
        _orbiter.m_XAxis.m_InputAxisName = "RightJoystickHorizontal";
    }

    private void Update()
    {
        MapInputs();
    }

    public void TurnCamera(float value)
    {
        if (Mathf.Abs(_orbiter.m_XAxis.Value) >= 1f)
            _orbiter.m_RecenterToTargetHeading.m_enabled = true;
        else
            _orbiter.m_RecenterToTargetHeading.m_enabled = false;
    }

    public void CameraReset()
    {
        _orbiter.m_XAxis.Value = 0;
    }

    #region MapInput
    public void MapInputs()
    {
        if (Input.GetButtonDown(Constants.Input.ResetCamera))
        {
            CameraReset();
            //  kartEvents.OnCameraTurnReset();
        }
        TurnCamera(Input.GetAxis(Constants.Input.TurnCamera));
        //  kartEvents.OnCameraTurnStart(Input.GetAxis(Constants.Input.TurnCamera));
    }
    #endregion
}
