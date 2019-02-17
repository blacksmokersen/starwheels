using System.Linq;
using UnityEngine;
using Bolt;

public class ObserversSetup : EntityBehaviour
{
    [SerializeField] private GameObject _objectToObserve;

    public override void Attached()
    {
        if (entity.isAttached && entity.isOwner)
        {
            SetObservers();
        }
    }

    // PRIVATE

    private void SetObservers()
    {
        foreach (var observer in FindObjectsOfType<MonoBehaviour>().OfType<IObserver>())
        {
            observer.Observe(_objectToObserve);
        }
    }
}
