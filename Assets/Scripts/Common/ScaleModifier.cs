using UnityEngine;

/*
 * May have to synchronize the size with a throwable state
 *
 */
public class ScaleModifier : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ScaleModifierSettings settings;

    private float _timer = 0f;
    private bool _modifyingScale = false;
    private Vector3 _increaseRatio;
    private Vector3 _startSize;

    // CORE

    private void Awake()
    {
        _increaseRatio = settings.TargetSize - settings.StartSize;
    }

    private void OnEnable()
    {
        _timer = 0f;
    }

    private void Update()
    {
        if (_timer < settings.SecondsBeforeFullSize && _modifyingScale)
        {
            _timer += Time.deltaTime;
            IncreaseSize();
        }
    }

    // PUBLIC

    public void ResetScale()
    {
     //   Debug.Log("Resetting scale");
     //   Debug.Log("Local scale before : " + transform.localScale);
        _modifyingScale = false;
        _timer = 0f;
        transform.localScale = settings.StartSize;
     //   Debug.Log("Local scale after : " + transform.localScale);
    }

    public void StartIncreasing()
    {
        _timer = 0f;
        _modifyingScale = true;
    }

    // PRIVATE

    private void IncreaseSize()
    {
        var currentScale = transform.localScale;
        currentScale.x = settings.StartSize.x + _timer / settings.SecondsBeforeFullSize * _increaseRatio.x;
        currentScale.y = settings.StartSize.y + _timer / settings.SecondsBeforeFullSize * _increaseRatio.y;
        currentScale.z = settings.StartSize.z + _timer / settings.SecondsBeforeFullSize * _increaseRatio.z;
        transform.localScale = currentScale;
    }
}
