using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Network.Ping
{
    [System.Serializable]
    public class PingEvent : UnityEvent<string, int> { }

    public class PingRequester : MonoBehaviour
    {
        [Header("Events")]
        public PingEvent OnPingUpdated;
        public StringEvent OnPingFailed;

        // PUBLIC

        public void RequestPingForAddress(string address, int timeout = 10)
        {
            StartCoroutine(RequestPingForAddressRoutine(address, timeout));
        }

        // PRIVATE

        private IEnumerator RequestPingForAddressRoutine(string address, int timeout)
        {
            var ping = new UnityEngine.Ping(address);
            var startingTime = Time.time;
            bool timeExceeded = false;

            while (!ping.isDone && !timeExceeded)
            {
                timeExceeded = (Time.time < startingTime + timeout);
                yield return new WaitForEndOfFrame();
            }

            if (ping.isDone && OnPingUpdated != null)
            {
                Debug.LogFormat("Ping found for {0} : {1}", address, ping.time);
                OnPingUpdated.Invoke(address, ping.time);
            }
            else if (timeExceeded && OnPingFailed != null)
            {
                Debug.LogFormat("Ping failed for {0} !", address);
                OnPingFailed.Invoke(address);
            }
        }
    }
}
