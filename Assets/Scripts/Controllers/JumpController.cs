using System;
using DG.Tweening;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Scripts
{
    internal class JumpController : MonoBehaviour
    {
        private bool _isJumping;
        private float _jumpStartTime;
        private bool _leftToRight;

        [Inject] private IMessageBus _messageBus;
        [Inject] private ObjectLocator _objectLocator;
        [Inject] private JumpSettings _jumpSettings;

        private void OnEnable()
        {
            _messageBus.Subscribe<StartJumpingMessage>(OnJumpStart);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<StartJumpingMessage>(OnJumpStart);
        }

        public void OnJumpStart(StartJumpingMessage _)
        {
            _isJumping = true;
            _jumpStartTime = Time.time;
            _leftToRight = _objectLocator.PlayerTransform.position.x < 0;
        }

        private void Update()
        {
            if (!_isJumping)
                return;

            float time = Time.time - _jumpStartTime;
            float position = _jumpSettings.GetPosition(time);

            var background = _objectLocator.BackgroundRect;
            var playerRect = _objectLocator.PlayerRect;

            var (startX, endX, offset) = _leftToRight
                ? (background.xMin, background.xMax, -playerRect.width / 2f)
                : (background.xMax, background.xMin, playerRect.width / 2f);

            var actualX = (position * (endX - startX)) + startX + offset;

            var player = _objectLocator.PlayerTransform;
            player.localPosition = new Vector3(actualX, player.localPosition.y);
        }
    }
}
