using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSayHello : MonoBehaviour
{
    [SerializeField] private Animator _animatorChar;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
            _animatorChar.SetTrigger("CharHello");
    }
}
