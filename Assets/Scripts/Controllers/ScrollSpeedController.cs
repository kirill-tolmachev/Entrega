using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using DG.Tweening;
using Scripts;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class ScrollSpeedController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;
        [Inject] private GlobalSettings _settings;

        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private object _tween;

        private void OnEnable()
        {
            SetSpeed(_minSpeed);
            _messageBus.Subscribe<ResetMessage>(OnReset);
            _messageBus.Subscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ResetMessage>(OnReset);
            _messageBus.Unsubscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
        }

        private void OnReset(ResetMessage _)
        {
            DOTween.Kill(_tween);
            _tween = null;

            SetSpeed(_minSpeed);
        }

        private void SetSpeed(float value)
        {
            _settings.ScrollSpeed = value;
        }

        private void OnLevelShouldChange(LevelShouldChangeMessage obj)
        {
            var level = obj.LevelIndex;
            float targetSpeed = Mathf.Clamp(level * 2f, _minSpeed, _maxSpeed);

            if (_tween != null)
            {
                DOTween.Kill(_tween);
            }

            _tween = DOTween.To(() => _settings.ScrollSpeed, SetSpeed, targetSpeed, 0.2f).SetEase(Ease.Linear);
        }
    }
}
