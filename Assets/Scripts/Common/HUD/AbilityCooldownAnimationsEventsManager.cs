using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;


public class AbilityCooldownAnimationsEventsManager : MonoBehaviour, IObserver
{
    private AbilitySetter _abilitySetter;

    //PUBLIC

    public void Observe(GameObject kartRoot)
    {
        _abilitySetter = kartRoot.GetComponentInChildren<AbilitySetter>();
    }

    public void TriggerCooldownResetAnimation(float cooldownAnimationDuration)
    {
        GetComponent<Animator>().SetTrigger("StartCooldown");
        GetComponent<Animator>().SetFloat("SpeedAnimMult", 1/cooldownAnimationDuration);
    }

    public void OnStartCooldownAnimation()
    {

    }

    public void OnCooldownCompleteAnimation()
    {
        _abilitySetter.GetCurrentAbility().OnCooldownCompleteAnimation();
    }
}

