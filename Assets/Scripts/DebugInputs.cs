using UnityEngine.SceneManagement;
using UnityEngine;

namespace Controls
{
    public class DebugInputs : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}