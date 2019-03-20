using UnityEngine;
using Bolt;
using Photon;

namespace Gamemodes
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
                //Debug.Log("GameMode set : " + roomToken.Gamemode);

                switch (roomToken.Gamemode)
                {
                    case Constants.Gamemodes.Battle:
                        _battleSpecifics.SetActive(true);
                        break;
                    case Constants.Gamemodes.FFA:
                        _battleSpecifics.SetActive(true);
                        break;
                    case Constants.Gamemodes.Totem:
                        _totemSpecifics.SetActive(true);
                        break;
                    default:
                        Debug.LogError("GameMode unknown !");
                        break;
                }
            }
            else
            {
                Debug.LogError("Couldn't find the attached token to set the gamemode.");
            }
        }
    }
}
