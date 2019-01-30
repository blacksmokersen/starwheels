using UnityEngine;

public class CloakPortalsActivator : MonoBehaviour
{
    public float TravelTime;
    public float TimeToUseThisPortalAgain;

    [SerializeField] GameObject _portal1;
    [SerializeField] GameObject _portal2;

    public void EnablePortals()
    {
        _portal1.SetActive(true);
        _portal2.SetActive(true);
    }

    public void DisablePortals()
    {
        _portal1.SetActive(false);
        _portal2.SetActive(false);
    }
}
