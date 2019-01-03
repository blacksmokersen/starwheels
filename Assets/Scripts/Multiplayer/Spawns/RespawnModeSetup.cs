using UnityEngine;
using Bolt;
using Photon;

namespace GameModes
{
    public class RespawnModeSetup : EntityBehaviour
    {
        [SerializeField] private GameObject _knockoutSpecifics;
        [SerializeField] private GameObject _destroyAndRespawnSpecifics;

        public override void Attached()
        {
            if (entity.attachToken != null)
            {
                var roomToken = (RoomProtocolToken)entity.attachToken;

                switch (roomToken.Gamemode)
                {
                    case Constants.GameModes.Battle:
                        _destroyAndRespawnSpecifics.SetActive(true);
                        break;
                    case Constants.GameModes.Totem:
                        _knockoutSpecifics.SetActive(true);
                        break;
                    default:
                        Debug.LogError("GameMode unknown !");
                        break;
                }
            }
            else
            {
                Debug.LogError("Couldn't find the attached token to set the respawnmode.");
            }
        }
    }
}
