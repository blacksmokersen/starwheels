using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class TotemChargeHUD : MonoBehaviour
{
    [SerializeField] private GameObject _charging;
    [SerializeField] private GameObject _dicharging;

    public void ShowChargingHUD()
    {
        StopAllCoroutines();
        StartCoroutine(ShowChargingRoutine());
    }

    public void ShowDischargingHUD()
    {
        StopAllCoroutines();
        StartCoroutine(ShowDischargingRoutine());
    }

    public void HideChargingHUD()
    {
        _charging.SetActive(false);
    }

    public void HideDischargingHUD()
    {
        _dicharging.SetActive(false);
    }


    private IEnumerator ShowChargingRoutine()
    {
        _dicharging.SetActive(false);
        _charging.SetActive(true);
        yield return new WaitForSeconds(3);
        _charging.SetActive(false);
    }

    private IEnumerator ShowDischargingRoutine()
    {
        _charging.SetActive(false);
        _dicharging.SetActive(true);
        yield return new WaitForSeconds(3);
        _dicharging.SetActive(false);
    }
}
