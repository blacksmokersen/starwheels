using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDisabler : MonoBehaviour
{
    public bool IsDisabled = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.B))
        {
            IsDisabled = !IsDisabled;
            SwitchDisabler();
        }
    }

    public void SwitchDisabler()
    {
        if (IsDisabled)
            GetComponent<Canvas>().enabled = false;
        else
            GetComponent<Canvas>().enabled = true;
    }
}
