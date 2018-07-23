using UnityEngine.SceneManagement;
using UnityEngine;
using Kart;

namespace Controls
{
    public class DebugInputs : MonoBehaviour
    {
        KartHealthSystem kartHealthSystem;
        GameObject kart;

        public void SetKart(KartHealthSystem value)
        {
            kartHealthSystem = value;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                kartHealthSystem.HealthLoss();
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                kartHealthSystem.Health = 3;
                kartHealthSystem.dead = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                kart = GameObject.FindWithTag("Kart");
                kart.transform.position = new Vector3(0,0.1f,0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                kart = GameObject.FindWithTag("Kart");
                kart.transform.position = new Vector3(-221, 3, 0);
            }
        }
    }
}