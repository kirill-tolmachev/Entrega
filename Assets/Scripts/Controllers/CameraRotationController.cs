using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Cinemachine;
using DG.Tweening;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Controllers
{
    internal class CameraRotationController : MonoBehaviour
    {
        [Inject] private CinemachineVirtualCamera _camera;
        [Inject] private IMessageBus _messageBus;
        
        [SerializeField] private float _rotationSpeed;

        private float _targetRotation;
        private bool _isFrozen;

        private float Rotation
        {
            get => _camera.m_Lens.Dutch;
            set => _camera.m_Lens.Dutch = value;
        }

        private void OnEnable()
        {
            _messageBus.Subscribe<ResetMessage>(OnReset);
            _messageBus.Subscribe<CreditsStarted>(OnCreditsStarted);
            _messageBus.Subscribe<CreditsEnded>(OnCreditsEnded);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ResetMessage>(OnReset);
            _messageBus.Unsubscribe<CreditsStarted>(OnCreditsStarted);
            _messageBus.Unsubscribe<CreditsEnded>(OnCreditsEnded);
        }

        private void OnReset(ResetMessage _)
        {
            _targetRotation = 0f;
            Rotation = 0f;
        }

        private void Update()
        {
            if (_isFrozen)
                return;

            if (Mathf.Abs(Rotation - _targetRotation) < 0.01)
            {
                _targetRotation = Random.value * 360f - 180f;
            }

            Rotation = Mathf.Lerp(Rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void OnCreditsStarted(CreditsStarted obj)
        {
            DOTween.To(() => Rotation, x => Rotation = x, 0f, 0.1f).SetUpdate(true);
            _isFrozen = true;
        }

        private void OnCreditsEnded(CreditsEnded obj)
        {
            _isFrozen = false;
        }
    }
}
