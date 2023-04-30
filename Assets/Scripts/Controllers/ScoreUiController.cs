using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class ScoreUiController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [Inject] private IMessageBus _messageBus;

        private void OnEnable()
        {
            _messageBus.Subscribe<LocalScoreChangedMessage>(OnScoreChanged);
        }


        private void OnDisable()
        {
            _messageBus.Subscribe<LocalScoreChangedMessage>(OnScoreChanged);
        }

        private void OnScoreChanged(LocalScoreChangedMessage message)
        {
            _scoreText.text = message.LocalScore.ToString(CultureInfo.InvariantCulture);
        }
    }
}
