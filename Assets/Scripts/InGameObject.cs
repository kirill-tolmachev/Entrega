using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal class InGameObject : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        public virtual void Start()
        {
            _messageBus.Publish(new ObjectCreatedMessage(transform)).Forget();
        }

        public virtual void OnDisable()
        {
            _messageBus.Publish(new ObjectDestroyedMessage(transform)).Forget();
        }
    }
}
