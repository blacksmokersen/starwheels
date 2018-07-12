using UnityEngine.SceneManagement;
using UnityEngine;
using Kart;

namespace Controls
{
    public class DebugInputs : MonoBehaviour
    {
        KartHealthSystem kartHealthSystem;

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
        }
    }
}