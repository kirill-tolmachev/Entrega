using Assets.Scripts.MessageImpl;
using Cysharp.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal class HpPickable : Pickable
    {
        [Inject] private IMessageBus _messageBus;

        protected override void OnPicked()
        {
            _messageBus.Publish(new PlayerHealRequestMessage(2)).Forget();
        }
    }
}