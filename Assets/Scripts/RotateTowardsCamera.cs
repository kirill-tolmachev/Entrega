using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    internal class RotateTowardsCamera : MonoBehaviour
    {
        [SerializeField]
        private float _minDeviation;

        [SerializeField]
        private float _maxDeviation;

        [SerializeField] private Transform _target;

        [Inject] private CinemachineVirtualCamera _virtualCamera;

        [SerializeField] private bool _rotateOnUpdate;

        private void Start()
        {
            Rotate();
        }

        private void Update()
        {
            if (!_rotateOnUpdate)
                return;

            Rotate();
        }

        private void Rotate()
        {
            var rotation = _virtualCamera.m_Lens.Dutch;
            var deviation = Random.Range(_minDeviation, _maxDeviation);
            _target.rotation = Quaternion.Euler(0f, 0f, rotation + deviation);
        }
    }
}
