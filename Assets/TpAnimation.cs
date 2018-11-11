using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpAnimation : MonoBehaviour {

    public Animation tpAnimation;
    public float animTime;

    public void Tp_Animation()
    {
        StartCoroutine(PlayAnimation());
    }

    public IEnumerator PlayAnimation()
    {
        tpAnimation.Play();
        yield return new WaitForSeconds(animTime);
        tpAnimation.Stop();
    }

}
