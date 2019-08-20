using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiplayer;
using Bolt;

public class TeamBattleJailButton : GlobalEventListener
{
    [SerializeField] private Team _jailButtonTeam;
    [SerializeField] private float _jailButtonCooldown;

    private bool _isJailButtonEnabled = true;

    //CORE

    private void OnTriggerEnter(Collider other)
    {
        //  if (entity.isAttached && entity.isOwner && other.CompareTag(Constants.Tag.KartCollider))
        //  {
        if (_isJailButtonEnabled)
        {
            if (other.CompareTag(Constants.Tag.KartCollider))
            {
                if (other.GetComponentInParent<PlayerInfo>().Team != _jailButtonTeam)
                {
                    JailButtonPushed jailButtonPushed = JailButtonPushed.Create();
                    jailButtonPushed.PlayerID = other.GetComponentInParent<PlayerInfo>().OwnerID;
                    jailButtonPushed.Team = _jailButtonTeam.ToString();
                    jailButtonPushed.Send();
                }
                else if (other.GetComponentInParent<PlayerInfo>().Team == _jailButtonTeam)
                {

                }
            }
        }
    }

    //BOLT

    public override void OnEvent(JailButtonPushed evnt)
    {
        if (evnt.Team == _jailButtonTeam.ToString())
        {
            StartCoroutine(JailButtonCooldown());
        }
    }

    //PRIVATE

    private IEnumerator JailButtonCooldown()
    {
        _isJailButtonEnabled = false;
        yield return new WaitForSeconds(_jailButtonCooldown);
        _isJailButtonEnabled = true;
    }
}
