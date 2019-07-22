using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Bolt;

public class CountDownDisplayer : GlobalEventListener
{
    [SerializeField] private TextMeshProUGUI _CountdownText;

    public override void OnEvent(LobbyCountdown evnt)
    {
        if (evnt.Time >= 1 && evnt.Time < 6)
        {
            _CountdownText.gameObject.SetActive(true);
            _CountdownText.text = " GAME START IN :  " + evnt.Time;
        }
        else if (evnt.Time == 0)
        {
            _CountdownText.text = " GAME STARTED !";
            StartCoroutine(GameStartTimerDisplay(2));
        }
    }

    private IEnumerator GameStartTimerDisplay(float timer)
    {
        yield return new WaitForSeconds(timer);
        _CountdownText.gameObject.SetActive(false);
    }
}
