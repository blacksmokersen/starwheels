using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Bolt;

public class CountDownDisplayer : GlobalEventListener
{
    [SerializeField] private TextMeshProUGUI _CountdownText;

    [SerializeField] private GameObject _CountDown3;
    [SerializeField] private GameObject _CountDown2;
    [SerializeField] private GameObject _CountDown1;
    [SerializeField] private GameObject _CountDownGO;

    public override void OnEvent(LobbyCountdown evnt)
    {
        if (evnt.Time == 3)
        {
            _CountDown3.SetActive(true);
            DisableCountDown(_CountDown3,1.1f);
        }
        if (evnt.Time == 2)
        {
            _CountDown2.SetActive(true);
            DisableCountDown(_CountDown2, 1.1f);
        }
        if (evnt.Time == 1)
        {
            _CountDown1.SetActive(true);
            DisableCountDown(_CountDown1, 1.1f);
        }
        if (evnt.Time == 0)
        {
            _CountDownGO.SetActive(true);
            DisableCountDown(_CountDownGO, 1.1f);
        }

        /*
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
        */
    }

    private IEnumerator GameStartTimerDisplay(float timer)
    {
        yield return new WaitForSeconds(timer);
        _CountdownText.gameObject.SetActive(false);
    }

    private IEnumerator DisableCountDown(GameObject countdownGameobject,float timer)
    {
        yield return new WaitForSeconds(timer);
        countdownGameobject.SetActive(false);
    }
}
