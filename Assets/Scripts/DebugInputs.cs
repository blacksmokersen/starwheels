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
            if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                kartHealthSystem.HealtLoss();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                kart = GameObject.FindWithTag("Kart");
                kart.transform.position = new Vector3(0,0.1f,0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                kart.transform.position = new Vector3(-221, 3, 0);
            }
        }
    }
}