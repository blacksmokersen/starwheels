using UnityEngine;

namespace CameraUtils
{
    public class FindCamera : MonoBehaviour
    {
        SetKartCamera setKartCamera;

        void Start()
        {
            setKartCamera = FindObjectOfType<SetKartCamera>();
            if (setKartCamera)
            {
                setKartCamera.SetKart(gameObject);
            }
        }
    }
}
