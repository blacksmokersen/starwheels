using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;


public class AbilityCooldownAnimationsEventsManager : MonoBehaviour, IObserver
{
    private AbilitySetter _abilitySetter;

    public void Observe(GameObject kartRoot)
    {
        _abilitySetter = kartRoot.GetComponentInChildren<AbilitySetter>();
    }

    public void TriggerCooldownResetAnimation(float cooldownAnimationDuration)
    {
        GetComponent<Animator>().SetTrigger("StartCooldown");
        StartCoroutine(TempCooldown());
      //  GetComponent<Animator>().SetFloat("SpeedAnimMult", 1 - cooldownAnimationDuration/10);
    }

    private IEnumerator TempCooldown()
    {
        yield return new WaitForSeconds(5);
        _abilitySetter.GetCurrentAbility().OnCooldownCompleteAnimation();
    }

    public void OnCooldownCompleteAnimation()
    {
        Debug.Log("TestReload");
        _abilitySetter.GetCurrentAbility().OnCooldownCompleteAnimation();
    }
}

