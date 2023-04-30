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
            _messageBus.Subscribe<TotalScoreChangedMessage>(OnScoreChanged);
        }


        private void OnDisable()
        {
            _messageBus.Subscribe<TotalScoreChangedMessage>(OnScoreChanged);
        }

        private void OnScoreChanged(TotalScoreChangedMessage message)
        {
            _scoreText.text = message.TotalScore.ToString(CultureInfo.InvariantCulture);
        }
    }
}
