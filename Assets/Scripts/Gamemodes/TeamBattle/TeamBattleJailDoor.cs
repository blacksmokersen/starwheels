using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiplayer;
using Bolt;

public class TeamBattleJailDoor : GlobalEventListener
{
    [SerializeField] private Team _doorTeam;
    private Animator _doorAnimator;
    private Coroutine _doorAnimatorCoroutine;

    //CORE

    private void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    //BOLT

    public override void OnEvent(JailButtonPushed evnt)
    {
        if (evnt.Team == _doorTeam.ToString())
        {
            _doorAnimatorCoroutine = StartCoroutine(OpenDoor());
        }
    }

    //PRIVATE

    private IEnumerator OpenDoor()
    {
        _doorAnimator.SetTrigger("OpenDoor");
        yield return new WaitForSeconds(5);
        _doorAnimator.SetTrigger("CloseDoor");
    }
}
