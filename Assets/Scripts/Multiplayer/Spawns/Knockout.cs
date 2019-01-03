using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Multiplayer
{
    public class Knockout : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnKnockout;

        public void ApplyKnockoutState()
        {
            OnKnockout.Invoke();
        }
    }
}
