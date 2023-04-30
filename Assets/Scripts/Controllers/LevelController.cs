using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Cysharp.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelSettings _settings;
        [Inject] private IMessageBus _messageBus;

        private int _currentLevel = 0;
        private int _requiredScore = 30;

        private void OnEnable()
        {
            _messageBus.Subscribe<LocalScoreChangedMessage>(OnScoreChanged);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<LocalScoreChangedMessage>(OnScoreChanged);
        }

        private void OnScoreChanged(LocalScoreChangedMessage message)
        {
            if (message.LocalScore < _requiredScore) 
                return;

            _requiredScore *= 2;
            _currentLevel++;
            var levelInfo = _settings.GetInfo(_currentLevel);
            _messageBus.Publish(new LevelShouldChangeMessage(_currentLevel, levelInfo, _requiredScore)).Forget();
        }


    }
}
