using UnityEngine;
using UnityEngine.Events;

public class SelfDestroyer : MonoBehaviour
{
    public float SecondsBeforeSelfDestruction;

    [Header("Unity Event")]
    public UnityEvent OnDestroyed;

    public void StartCountdown()
    {
        Destroy(gameObject, SecondsBeforeSelfDestruction);
    }

    public void StartCountdown(float seconds)
    {
        Destroy(gameObject, seconds);
    }

    private void OnDestroy()
    {
        OnDestroyed.Invoke();
    }
}
