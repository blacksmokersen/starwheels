using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using CameraUtils;
using Multiplayer;

namespace Gamemodes
{
    public class EndGameDisplayer : GlobalEventListener
    {
        [SerializeField] private GameObject _displayCamera;
        [SerializeField] private List<GameObject> _displays = new List<GameObject>();
        [SerializeField] private List<Transform> _standPositions = new List<Transform>();

        // private Dictionary<string, Transform> _respawns = new Dictionary<string, Transform>();

        public override void OnEvent(GameOver evnt)
        {
            foreach (GameObject display in _displays)
            {
                display.SetActive(true);
            }
            _displayCamera.SetActive(true);

            var standNumber = 0;
            foreach (GameObject kart in SWExtensions.KartExtensions.GetKartsWithTeam((Team)evnt.WinningTeam))
            {
                kart.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                kart.gameObject.transform.position = _standPositions[standNumber].position;
                standNumber++;
            }
        }
    }
}
