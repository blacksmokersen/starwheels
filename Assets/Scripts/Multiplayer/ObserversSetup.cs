using System.Linq;
using UnityEngine;
using Bolt;

public class ObserversSetup : EntityBehaviour
{
    public bool IsEnabled = true;

    [SerializeField] private GameObject _objectToObserve;

    public override void Attached()
    {
        if (entity.isAttached && entity.isOwner && IsEnabled)
        {
            SetObservers();
        }
    }

    // PUBLIC

    public void SetObservers()
    {
        foreach (var observer in FindObjectsOfType<MonoBehaviour>().OfType<IObserver>())
        {
            observer.Observe(_objectToObserve);
        }
    }
}
