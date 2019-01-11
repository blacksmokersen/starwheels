using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float SecondsBeforeSelfDestruction;

    public void StartCountdown()
    {
        Destroy(gameObject, SecondsBeforeSelfDestruction);
    }

    public void StartCountdown(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
