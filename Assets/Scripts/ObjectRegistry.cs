using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal class ObjectRegistry : IDisposable
    {
        private readonly IMessageBus _messageBus;

        private readonly HashSet<Transform> _objects = new();

        public ObjectRegistry(IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _messageBus.Subscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Subscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
        }

        public IEnumerable<Transform> Objects => _objects;

        private void OnObjectCreated(ObjectCreatedMessage message)
        {
            _objects.Add(message.Object);
        }

        private void OnObjectDestroyed(ObjectDestroyedMessage message)
        {
            _objects.Remove(message.Object);
        }

        public void Dispose()
        {
            _messageBus.Unsubscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Unsubscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
        }
    }
}
