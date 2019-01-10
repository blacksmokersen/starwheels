using UnityEngine;
using Bolt;
using Items;
using Engine;

namespace CameraUtils
{
    public class FindCamera : EntityBehaviour
    {
        public override void ControlGained()
        {
            SetKartCamera setKartCamera = FindObjectOfType<SetKartCamera>();
            if (setKartCamera)
            {
                setKartCamera.SetKart(gameObject);
            }
            else
            {
                Debug.LogWarning("Camera not found.");
            }

            CamSpeedEffect camSpeedEffect = FindObjectOfType<CamSpeedEffect>();
            if (camSpeedEffect)
            {
                camSpeedEffect.SetRigidbody(GetComponent<Rigidbody>());
            }
            else
            {
                Debug.LogWarning("CamSpeedEffect not found.");
            }

            ItemHUD itemHUD = FindObjectOfType<ItemHUD>();
            if (itemHUD)
            {
                itemHUD.ObserveKart(gameObject);
            }
            else
            {
                Debug.LogWarning("Item HUD not found.");
            }

            EngineHUD engineHUD = FindObjectOfType<EngineHUD>();
            if (engineHUD)
            {
                engineHUD.ObserveKart(gameObject);
            }
            else
            {
                Debug.LogWarning("Engine HUD not found.");
            }
        }
    }
}
