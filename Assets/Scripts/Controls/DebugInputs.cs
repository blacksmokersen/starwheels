using UnityEngine.SceneManagement;
using UnityEngine;
using Kart;

namespace Controls
{
    public class DebugInputs : BaseKartComponent
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
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                kart = GameObject.FindWithTag("Kart");
                kart.transform.position = new Vector3(0,0.1f,0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                kart = GameObject.FindWithTag("Kart");
                kart.transform.position = new Vector3(-221, 3, 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                kart = GameObject.FindWithTag("Kart");
                kart.transform.position = new Vector3(400, 3, 0);
            }
            if (Input.GetButtonDown(Constants.SpecialCapacity))
            {
                StartCoroutine(GetComponent<GamepadVibrations>().Vibrate(0.2f, 0.5f));
            }
        }
    }
}