using UnityEngine.SceneManagement;
using UnityEngine;
using Abilities;

namespace Controls
{
    public class DebugInputs : BaseKartComponent
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            // Maps
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Kart().transform.position = new Vector3(0,0.1f,0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Kart().transform.position = new Vector3(-221, 3, 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Kart().transform.position = new Vector3(400, 3, 0);
            }

            // Abilities
            if (Input.GetButtonDown(Constants.UseAbilityButton))
            {
                StartCoroutine(GetComponent<GamepadVibrations>().Vibrate(0.2f, 0.5f));
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                ReplaceAbility().AddComponent<TPBackAbility>();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                ReplaceAbility().AddComponent<JumpAbility>();
            }
        }

        private GameObject Kart()
        {
            return GameObject.FindWithTag("Kart");
        }

        private GameObject ReplaceAbility()
        {
            var kart = Kart();
            Destroy(kart.GetComponentInChildren<Ability>());

            return kart.transform.GetChild(0).gameObject;
        }
    }
}
