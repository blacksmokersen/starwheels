using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Knockout
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
