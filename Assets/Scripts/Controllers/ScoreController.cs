using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class ScoreController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;
        
        private void OnEnable()
        {
            _messageBus.Subscribe<PackageDeliveredMessage>(OnPackageDelivered);
        }

        private void OnDisable()
        {
            _messageBus.Subscribe<PackageDeliveredMessage>(OnPackageDelivered);
        }

        private void OnPackageDelivered(PackageDeliveredMessage message)
        {
            _messageBus.Publish(new LocalScoreChangedMessage(10));
        }
    }
}
