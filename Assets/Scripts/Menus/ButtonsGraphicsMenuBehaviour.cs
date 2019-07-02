using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonsGraphicsMenuBehaviour : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    //PUBLIC

    public void StartMouseEnterAnimation()
    {
        _animator.ResetTrigger("OnMouseExit");
        _animator.SetTrigger("OnMouseEnter");
    }

    public void StartMouseExitAnimation()
    {
        _animator.ResetTrigger("OnMouseEnter");
        _animator.SetTrigger("OnMouseExit");
    }

    public void StartMouseClickAnimation()
    {
        _animator.SetTrigger("OnClick");
    }
}
