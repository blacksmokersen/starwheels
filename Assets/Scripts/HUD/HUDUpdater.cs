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
        public GameObject Compteur;

        private Compteur compteur;
        private Rigidbody kartRigidBody;

        public void SetKart(Rigidbody body)
        {
            compteur = Compteur.GetComponent<Compteur>();
            kartRigidBody = body;
            StartCoroutine(UpdateRoutine());
            //   StartCoroutine(UpdateVelocityRoutine());
        }

        IEnumerator UpdateRoutine()
        {
            while (Application.isPlaying)
            {
                TimeText.text = "Time : " + Time.time;
                SpeedText.text = "Speed : " + kartRigidBody.velocity.magnitude;
                FPSText.text = "FPS : " + 1.0f / Time.deltaTime;
                compteur.CompteurBehaviour(kartRigidBody.velocity.magnitude);
                yield return new WaitForSeconds(0.05f);
            }
        }
        /*
        IEnumerator UpdateVelocityRoutine()
        {
            while (Application.isPlaying)
            {
                compteur.CompteurBehaviour(kartRigidBody.velocity.magnitude);
                yield return new WaitForSeconds(0.05f);
            }
        }
        */
    }
}