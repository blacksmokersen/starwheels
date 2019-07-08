using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SW.Customization;

public class TauntAnimationScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _tauntSpamProtectionDuration;

    [Header("Dependencies")]
    [SerializeField] private CharacterSetter _characterSetter;
    [SerializeField] private Audio.VoiceLaneManager _voicelineManager;

    private bool _tauntSpamProtection = false;

    //CORE

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input.TauntInput) && !_tauntSpamProtection)
        {
            SetTriggerTauntAnimation();
            _voicelineManager.PlayRandomHitVoice();
            StartCoroutine(TauntSpamProtectionCoroutine());
        }
    }

    //PUBLIC

    public void SetTriggerTauntAnimation()
    {
        _characterSetter.CurrentCharacterAnimator.SetTrigger("Trigger1");
    }

    //PRIVATE

    private IEnumerator TauntSpamProtectionCoroutine()
    {
        _tauntSpamProtection = true;
        yield return new WaitForSeconds(_tauntSpamProtectionDuration);
        _tauntSpamProtection = false;
    }
}
