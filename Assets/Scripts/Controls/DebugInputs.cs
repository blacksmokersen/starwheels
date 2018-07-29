using UnityEngine.SceneManagement;
using UnityEngine;
using Kart;

namespace Controls
{
    public class DebugInputs : MonoBehaviour
    {
        KartHealthSystem kartHealthSystem;
        GameObject kart;
        public CinemachineDynamicScript Cam;

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
                kart.transform.position = new Vector3(411, 0, 0);
            }
            if (Input.GetButtonDown(Constants.SpecialCapacity))
            {
                StartCoroutine(GetComponent<GamepadVibrations>().Vibrate(0.2f, 0.5f));
            }
            if (Input.GetKey(KeyCode.O))
            {
                Cam.IonBeamCameraBehaviour(true);
            }
            if (Input.GetKey(KeyCode.P))
            {
                Cam.IonBeamCameraBehaviour(false);
            }
        }
    }
}