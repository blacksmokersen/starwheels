using System.Collections;
using UnityEngine;

namespace PlayerCamera
{
    [RequireComponent(typeof(Camera))]
    public class CameraShake : MonoBehaviour
    {
        public float ContinuousShakeMagnitude;
        private const float TIME_BETWEEN_SHAKES = 0.25f;

        private void Start()
        {
            StartCoroutine(ContinuousShake());
        }

        IEnumerator ContinuousShake()
        {
            while (Application.isPlaying)
            {
                StartCoroutine(OneShotShake(TIME_BETWEEN_SHAKES, ContinuousShakeMagnitude));
                yield return new WaitForSeconds(TIME_BETWEEN_SHAKES);
            }
        }

        IEnumerator OneShotShake(float duration, float magnitude)
        {
            var originalPosition = transform.localPosition;
            float timer = 0.0f;

            while(timer < duration)
            {
                float dx = Random.Range(-1f, 1f) * magnitude; // Trop violent : Utiliser perlin noise peut-être
                float dy = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(dx, dy, originalPosition.z);
                timer += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPosition;
        }
    }
}