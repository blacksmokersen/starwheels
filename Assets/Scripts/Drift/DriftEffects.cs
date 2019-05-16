using UnityEngine;

namespace Drift
{
    public class DriftEffects : MonoBehaviour
    {
        /*
        [Header("Wheels Smoke Prefabs")]
        [SerializeField] private GameObject _blueLeftSmoke;
        [SerializeField] private GameObject _blueRightSmoke;
        [SerializeField] private GameObject _redLeftSmoke;
        [SerializeField] private GameObject _redRightSmoke;
        */
        [Header("Kart Animator")]
        [SerializeField] private Animator _kartAnimator;
        /*
        private GameObject _currentLeftSmoke;
        private GameObject _currentRightSmoke;
        */
        // PUBLIC

        public void StopSmoke()
        {
            _kartAnimator.SetBool("DriftBlue", false);
            _kartAnimator.SetBool("DriftRed", false);
            /*
            if (_currentLeftSmoke) _currentLeftSmoke.SetActive(false);
            _currentLeftSmoke = null;
            if (_currentRightSmoke) _currentRightSmoke.SetActive(false);
            _currentRightSmoke = null;
            */
        }

        public void StartSmoke()
        {
            /*
            if (_currentLeftSmoke) _currentLeftSmoke.SetActive(true);
            if (_currentRightSmoke) _currentRightSmoke.SetActive(true);
            */
        }

        public void SetWheelsBlue()
        {
            _kartAnimator.SetBool("DriftRed", false);
            _kartAnimator.SetBool("DriftBlue", true);
            /*
            StopSmoke();
            _currentLeftSmoke = _blueLeftSmoke;
            _currentRightSmoke = _blueRightSmoke;
            StartSmoke();
            */
        }

        public void SetWheelsRed()
        {
            _kartAnimator.SetBool("DriftBlue", false);
            _kartAnimator.SetBool("DriftRed", true);
            /*
            StopSmoke();
            _currentLeftSmoke = _redLeftSmoke;
            _currentRightSmoke = _redRightSmoke;
            StartSmoke();
            */
        }
    }
}
