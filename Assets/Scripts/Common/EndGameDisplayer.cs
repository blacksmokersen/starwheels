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
        [SerializeField] private Transform _kartPosition;

        public override void OnEvent(GameOver evnt)
        {
            _display.SetActive(true);
            _displayCamera.SetActive(true);

            foreach (GameObject kart in SWExtensions.KartExtensions.GetKartsWithTeam((Team)evnt.WinningTeam))
            {
                kart.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                kart.gameObject.transform.position = _kartPosition.position;
            }
        }
    }
}
