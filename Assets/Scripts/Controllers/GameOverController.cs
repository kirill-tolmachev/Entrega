using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Assets.Scripts.MessageImpl;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts;
using Scripts.Infrastructure.Messages;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class GameOverController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _gameOverFill;

        [Inject] private IMessageBus _messageBus;

        private void OnEnable()
        {
            _messageBus.Subscribe<DeathMessage>(OnDeath);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<DeathMessage>(OnDeath);
        }

        private void OnDeath(DeathMessage message)
        {
            if (!message.Target.TryGetComponent(out Player _))
                return;

            OnGameOver();
        }

        private void OnGameOver()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_gameOverFill.transform.DOLocalMoveY(-0.5f, 0.2f).SetEase(Ease.InOutCubic).SetDelay(1f));
            sequence.AppendCallback(Reset);
            sequence.AppendInterval(1f);
            sequence.Append(_gameOverFill.transform.DOLocalMoveY(0.5f, 0.2f).SetEase(Ease.InOutCubic));
            sequence.AppendCallback(() => _gameOverFill.transform.localPosition = new Vector3(_gameOverFill.transform.localPosition.x, -1.5f, _gameOverFill.transform.localPosition.z));

            sequence.Play();

        }

        private void Reset()
        {
            _messageBus.Publish(new ResetMessage()).Forget();
        }
    }
}
