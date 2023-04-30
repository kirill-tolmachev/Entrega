using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class HealController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;
        
        private void OnEnable()
        {
            _messageBus.Subscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
        }

        private void OnLevelShouldChange(LevelShouldChangeMessage _)
        {
            _messageBus.Publish(new PlayerHealRequestMessage(5));
        }
    }
}
