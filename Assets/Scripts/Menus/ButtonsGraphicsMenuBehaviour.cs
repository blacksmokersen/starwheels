using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonsGraphicsMenuBehaviour : MonoBehaviour
{
    private Animator _animator;

    //CORE

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //PUBLIC

    public void StartMouseEnterAnimation()
    {
        _animator.SetTrigger("OnMouseEnter");
    }

    public void StartMouseExitAnimation()
    {
        _animator.SetTrigger("OnMouseExit");
    }

    public void StartMouseClickAnimation()
    {
        _animator.SetTrigger("OnClick");
    }
}
