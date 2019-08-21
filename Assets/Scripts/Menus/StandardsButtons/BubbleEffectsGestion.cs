using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.Options
{
    public class BubbleEffectsGestion : MonoBehaviour
    {
        [SerializeField] private Transform[] _bubles;
        private Vector3 _destination;

        [SerializeField] private float _bubbleSpeed = 3.0f;
        [SerializeField] private float _maxDistance = 250.0f;

        private void OnEnable()
        {
            foreach (Transform _buble in _bubles)
            {
                _buble.position = transform.position;
            }
        }

        private void Update()
        {
            GetDirection();
        }

        private void GetDirection()
        {
            _destination = new Vector3(Input.mousePosition.x, 0, 0) - new Vector3(_bubles[0].position.x, 0,0);
            if ((_bubles[0].position.x < -_maxDistance && _destination.x < 0) ||( _bubles[0].position.x > _maxDistance && _destination.x > 0))
            {
                _destination = new Vector3(_bubles[0].position.x, 0, 0); // position constraints
            }
            else
            {
                AllBublePosition();
            }
        }

        private void AllBublePosition()
        {
            _bubles[0].transform.position += _destination * _bubbleSpeed * Time.deltaTime;

            for (int i = 1; i < _bubles.Length; i++)
            {
                _bubles[i].position = _bubles[0].position + ((float)(i + 1) / _bubles.Length) * (2 *(transform.position - _bubles[0].position)); // Each buble is placed on equal distance form each over on symmetry line which center is the buttons center and one extremity is the first buble
            }
        }
    }
}