using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace HUD
{
    public class HUDUpdater : MonoBehaviour
    {
        public Text SpeedText;
        public Text TimeText;
        public Text FPSText;

        private Rigidbody kartRigidBody;

        public void SetKart(Rigidbody body)
        {
            kartRigidBody = body;
            StartCoroutine(UpdateRoutine());
        }

        IEnumerator UpdateRoutine()
        {
            while (Application.isPlaying)
            {
                TimeText.text = "Time : " + Time.time;
                SpeedText.text = "Speed : " + kartRigidBody.velocity.magnitude;
                FPSText.text = "FPS : " + 1.0f / Time.deltaTime;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}