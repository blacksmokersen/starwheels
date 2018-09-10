using UnityEngine.SceneManagement;
using UnityEngine;
using Abilities;
using Photon.Pun;

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
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Kart().transform.position = new Vector3(0,0.1f,0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Kart().transform.position = new Vector3(-221, 3, 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                Kart().transform.position = new Vector3(400, 3, 0);
            }

            // Abilities
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                GetComponent<GamepadVibrations>().SmallVibration();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                ReplaceAbility().AddComponent<AbilityTPBack>();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                ReplaceAbility().AddComponent<AbilityJump>();
            }
        }

        private GameObject Kart()
        {
            foreach (GameObject kart in GameObject.FindGameObjectsWithTag("Kart"))
            {
                if (kart.GetComponent<PhotonView>().IsMine) return kart;
            }

            return GameObject.FindGameObjectWithTag("Kart");
        }

        private GameObject ReplaceAbility()
        {
            var kart = Kart();
            Destroy(kart.GetComponentInChildren<Ability>());

            return kart.transform.GetChild(0).gameObject;
        }
    }
}
