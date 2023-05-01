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

        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            _messageBus.Subscribe<DeathMessage>(OnDeath);
            _messageBus.Subscribe<CreditsEnded>(OnCreditsEnded);
        }


        private void OnDisable()
        {
            _messageBus.Unsubscribe<DeathMessage>(OnDeath);
            _messageBus.Unsubscribe<CreditsEnded>(OnCreditsEnded);
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
            sequence.AppendCallback(() => PlayerController.InputLocked = true);
            sequence.Append(_gameOverFill.transform.DOLocalMoveY(-0.5f, 0.2f).SetEase(Ease.InOutCubic).SetDelay(1f));
            sequence.AppendCallback(Reset);
            sequence.AppendInterval(1f);
            sequence.Append(_gameOverFill.transform.DOLocalMoveY(0.5f, 0.2f).SetEase(Ease.InOutCubic));
            sequence.AppendCallback(OnPlay);
            sequence.AppendCallback(() => _gameOverFill.transform.localPosition = new Vector3(_gameOverFill.transform.localPosition.x, -1.5f, _gameOverFill.transform.localPosition.z));
            sequence.AppendCallback(() => PlayerController.InputLocked = false);
            sequence.Play();
        }

        private void OnCreditsEnded(CreditsEnded _)
        {
            Reset();
            OnPlay();
        }

        private void Reset()
        {
            _audioSource.Play();
            _messageBus.Publish(new ResetMessage()).Forget();
        }

        private void OnPlay()
        {
            _messageBus.Publish(new OnPlayMessage()).Forget();
        }
    }
}
