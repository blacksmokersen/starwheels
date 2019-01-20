using UnityEngine;

/*
 * May have to synchronize the size with a throwable state
 *
 */
public class SizeIncrease : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SizeIncreaseSettings settings;

    private float _timer = 0f;
    private Vector3 _increaseRatio;

    // CORE

    private void Awake()
    {
        _increaseRatio = settings.TargetSize - settings.StartSize;
    }

    private void Update()
    {
        if (_timer < settings.SecondsBeforeFullSize)
        {
            _timer += Time.deltaTime;
            IncreaseSize();
        }
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
