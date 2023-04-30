using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Controllers
{
    internal class CameraRotationController : MonoBehaviour
    {
        [Inject] private CinemachineVirtualCamera _camera;

        [SerializeField] private float _rotationSpeed;

        private float _targetRotation;

        private float Rotation
        {
            get => _camera.m_Lens.Dutch;
            set => _camera.m_Lens.Dutch = value;
        }

        private void Update()
        {
            if (Mathf.Abs(Rotation - _targetRotation) < 0.01)
            {
                _targetRotation = Random.value * 360f - 180f;
            }

            Rotation = Mathf.Lerp(Rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
        }

    }
}
