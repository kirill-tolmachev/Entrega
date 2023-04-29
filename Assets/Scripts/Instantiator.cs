using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal class Instantiator
    {
        private readonly DiContainer _container;
        private readonly IMessageBus _messageBus;

        public Instantiator(DiContainer container, IMessageBus messageBus)
        {
            _container = container;
            _messageBus = messageBus;
        }

        public T InstantiatePrefab<T>(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
            var instance = _container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
            //_messageBus.Publish(new ObjectCreatedMessage(instance.transform)).Forget();

            return instance;
        }
    }
}
