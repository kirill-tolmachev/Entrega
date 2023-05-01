using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal class BonusPickable : Pickable
    {
        [Inject] private IMessageBus _messageBus;

        protected override void OnPicked()
        {
            _messageBus.Publish(new LocalScoreChangedMessage(10));
        }
    }
}