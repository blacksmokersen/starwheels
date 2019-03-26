using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine;

namespace CameraUtils
{
    public class FreeCamera : MonoBehaviour, IControllable, IObserver
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [SerializeField] private bool _switchInputs;

        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _verticalSpeed;
        [SerializeField] private float _rotateYSpeed;
        [SerializeField] private float _rotateXSpeed;
        [SerializeField] private float _forwardSpeed;
        [SerializeField] private float _backwardSpeed;

        private Camera _camera;
        private GameObject _kart;

        //CORE

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            MapInputs();
        }

        //PUBLIC

        public void Observe(GameObject kartroot)
        {
            _kart = kartroot;
        }

        public void MapInputs()
        {
            if (Enabled)
            {
                transform.eulerAngles += new Vector3(0, Input.GetAxis(Constants.Input.TurnCamera) * _rotateXSpeed, 0);
                transform.eulerAngles += new Vector3(-Input.GetAxis(Constants.Input.UpAndDownCamera) * _rotateYSpeed, 0, 0);

                if (_switchInputs)
                {
                    transform.Translate(Vector3.forward * Input.GetAxis(Constants.Input.Accelerate) * _forwardSpeed);
                    transform.Translate(Vector3.back * Input.GetAxis(Constants.Input.Decelerate) * _backwardSpeed);
                    transform.Translate(Vector3.right * Input.GetAxis(Constants.Input.TurnAxis) * _horizontalSpeed);
                    transform.Translate(Vector3.up * Input.GetAxis(Constants.Input.UpAndDownAxis) * _verticalSpeed);
                }
                else
                {
                    transform.Translate(Vector3.forward * Input.GetAxis(Constants.Input.UpAndDownAxis) * _forwardSpeed);
                    transform.Translate(Vector3.right * Input.GetAxis(Constants.Input.TurnAxis) * _horizontalSpeed);
                    transform.Translate(Vector3.up * Input.GetAxis(Constants.Input.Accelerate) * _verticalSpeed);
                    transform.Translate(Vector3.down * Input.GetAxis(Constants.Input.Decelerate) * _verticalSpeed);
                }
            }
        }

        public void DisableKartControls()
        {
            _kart.GetComponentInChildren<EngineBehaviour>().Enabled = false;
        }
        public void EnableKartControls()
        {
            _kart.GetComponentInChildren<EngineBehaviour>().Enabled = true;
        }
    }
}
