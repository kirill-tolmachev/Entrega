using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts;
using Scripts.Infrastructure.Messages;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class GameCompletedController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;
        [Inject] private GlobalSettings _globalSettings;
        [Inject] private CinemachineVirtualCamera _camera;

        [SerializeField] private Transform _menu;
        [SerializeField] private TMP_Text _text;

        private readonly string[] _texts = 
        {
            "Thank you!",
            "The final parcel is delivered",
            "Now we have a chance",
            "At least for now",
            "Entrega",
            "Made in 48 hours by dysleixc",
            "Ludum Dare 53",
            "Stop war in Ukraine"
        };

        private void OnEnable()
        {
            _messageBus.Subscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
        }

        private void OnLevelShouldChange(LevelShouldChangeMessage message)
        {
            if (message.LevelIndex < _globalSettings.LevelsCount)
                return;

            _messageBus.Publish(new CreditsStarted()).Forget();
            
            _menu.gameObject.SetActive(true);
            Time.timeScale = 0f;
            var sequence = DOTween.Sequence().SetUpdate(true).SetAutoKill(true);

            sequence.Append(_menu.DOScale(new Vector3(10f, 1f, 1f), 0.2f));
            foreach (var text in _texts)
            {
                sequence.AppendCallback(() => _text.text = text);
                sequence.Append(_text.DOFade(1f, 0.1f));
                sequence.AppendInterval(2f);
                sequence.Append(_text.DOFade(0f, 0.1f));
            }

            sequence.AppendCallback(OnCreditsCompleted);
            sequence.Append(_menu.DOScale(Vector3.zero, 0.2f));
            sequence.Play();
        }

        private void OnCreditsCompleted()
        {
            Time.timeScale = 1f;
            _messageBus.Publish(new CreditsEnded()).Forget();
        }


    }
}
