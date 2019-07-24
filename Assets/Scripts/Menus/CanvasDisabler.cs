using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class CanvasDisabler : GlobalEventListener
{
    public bool IsDisabled = false;
    [SerializeField] private CountdownSettings _countdownSettings;

    //CORE

    private void Start()
    {
        StartCoroutine(ForcePlayerHUD());

        if (BoltNetwork.IsConnected)
        {
            if (!_countdownSettings.Countdown)
                SwitchDisabler();
        }
        else
            SwitchDisabler();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.B))
        {
            IsDisabled = !IsDisabled;
            SwitchDisabler();
        }
    }

    //BOLT

    public override void OnEvent(OnAllPlayersInGame evnt)
    {
        if (evnt.IsGameAlreadyStarted)
            SwitchDisabler();
    }

    public override void OnEvent(LobbyCountdown evnt)
    {
        if (evnt.Time == 5)
        {
            SwitchDisabler();
        }
    }

    //PUBLIC

    public void SwitchDisabler()
    {
        if (IsDisabled)
            GetComponent<Canvas>().enabled = false;
        else
            GetComponent<Canvas>().enabled = true;
    }

    private IEnumerator ForcePlayerHUD()
    {
        yield return new WaitForSeconds(15);
        GetComponent<Canvas>().enabled = true;
    }
}
