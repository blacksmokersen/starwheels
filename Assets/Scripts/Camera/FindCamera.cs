using UnityEngine;
using Bolt;
using Items;

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

            CamSpeedEffect camSpeedEffect = FindObjectOfType<CamSpeedEffect>();
            if (camSpeedEffect)
            {
                camSpeedEffect.SetRigidbody(GetComponent<Rigidbody>());
            }

            ItemHUD itemHUD = FindObjectOfType<ItemHUD>();
            if (itemHUD)
            {
                itemHUD.ObserveKart(gameObject);
            }
        }
    }
}
