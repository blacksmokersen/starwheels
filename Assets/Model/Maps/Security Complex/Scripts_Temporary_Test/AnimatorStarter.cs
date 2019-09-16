using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorStarter : MonoBehaviour
{
    public Animator _Animator;
    public string[] _triggers;
    public UnityEvent _StartEvent;
    public bool _Loop;
    public float _CooldownDuration = 5.0f;

    void Start()
    {
        if (_triggers.Length > 0)
        {
            SetAnimationTrigger(0);
        }
        if (_StartEvent != null)
        {
            _StartEvent.Invoke();
        }
        if (_Loop)
        {
            StartCoroutine(AnimationLoop());
        }
    }

    public void SetAnimationTrigger(int _trigger)
    {
        _Animator.SetTrigger(_triggers[_trigger]);
    }

    private IEnumerator AnimationLoop()
    {
        yield return new WaitForSeconds(_CooldownDuration);
        _Animator.SetTrigger(0);
        StartCoroutine(AnimationLoop());
    }
}
