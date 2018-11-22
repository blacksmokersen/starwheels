using UnityEngine;
using Bolt;
using Photon;

namespace GameModes
{
    public class KartGameModeSetup : EntityBehaviour
    {
        [SerializeField] private GameObject _totemSpecifics;
        [SerializeField] private GameObject _battleSpecifics;

        public override void Attached()
        {
            if (entity.attachToken != null)
            {
                var roomToken = (RoomProtocolToken)entity.attachToken;
                switch (roomToken.Gamemode)
                {
                    case Constants.GameModes.Battle:
                        _battleSpecifics.SetActive(true);
                        break;
                    case Constants.GameModes.Totem:
                        _totemSpecifics.SetActive(false);
                        break;
                    default:
                        Debug.LogError("GameMode unknown !");
                        break;
                }
            }
        }
    }
}
