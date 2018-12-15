using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class IonBeamAnimatorEventsManager : MonoBehaviour {

        [SerializeField] private IonBeamLaserBehaviour _ionBeamLaserBehaviour;

        public void onStartAnimation()
        {
            _ionBeamLaserBehaviour.AtLaunchAnimation();
        }

        public void onExplodeAnimation()
        {
            _ionBeamLaserBehaviour.AtDamageAnimation();
        }

        public void onEndDamageAnimation()
        {
            _ionBeamLaserBehaviour.AtEndDamageAnimation();
        }

        public void onEndAnimation()
        {
            _ionBeamLaserBehaviour.AtEndAnimation();
        }
    }
}
