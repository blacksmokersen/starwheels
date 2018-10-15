using UnityEngine;

namespace CameraUtils
{
    public class FindCamera : MonoBehaviour
    {
        void Start()
        {
            SetKartCamera setKartCamera = FindObjectOfType<SetKartCamera>();
            if (setKartCamera)
            {
                setKartCamera.SetKart(gameObject);
            }
            CamSpeedEffect camSpeedEffect = FindObjectOfType<CamSpeedEffect>();
            if (camSpeedEffect)
            {
                camSpeedEffect.SetRigidbody(GetComponent<Rigidbody>());
            }
        }
    }
}
