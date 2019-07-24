using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using CameraUtils;

namespace Gamemodes
{
    public class EndGameDisplayer : GlobalEventListener
    {
        [SerializeField] private GameObject _display;
        [SerializeField] private GameObject _displayCamera;

        public override void OnEvent(GameOver evnt)
        {
            _display.SetActive(true);
            _displayCamera.SetActive(true);

            if (BoltNetwork.IsServer)
            {
                foreach (GameObject kart in SWExtensions.KartExtensions.GetKartsWithTeam((Team)evnt.WinningTeam))
                {
                    kart.gameObject.transform.position = new Vector3(_display.transform.position.x, _display.transform.position.y + 2, _display.transform.position.z);
                }
            }
        }
    }
}
