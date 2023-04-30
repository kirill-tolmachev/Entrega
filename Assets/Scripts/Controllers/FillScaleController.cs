using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using DG.Tweening;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class FillScaleController : MonoBehaviour
    {
        [SerializeField] private Transform _fillTransform;

        [Inject] private IMessageBus _messageBus;

        private int _requiredScore = 30;

        private void OnEnable()
        {
            _messageBus.Subscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
            _messageBus.Subscribe<TotalScoreChangedMessage>(OnScoreChanged);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
            _messageBus.Unsubscribe<TotalScoreChangedMessage>(OnScoreChanged);
        }

        private void OnLevelShouldChange(LevelShouldChangeMessage message)
        {
            _requiredScore = message.RequiredScore;
            _fillTransform.localScale = GetScaleValue(0f);
        }

        private void OnScoreChanged(TotalScoreChangedMessage message)
        {
            float scale = (message.LocalScore / (float)_requiredScore);
            _fillTransform.DOScale(GetScaleValue(scale), 0.1f).SetEase(Ease.OutElastic);
        }

        private Vector3 GetScaleValue(float scale) => new(scale, 1f, 1f);



    }
}
