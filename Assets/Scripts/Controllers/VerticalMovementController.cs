using Assets.Scripts;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using System.Collections.Generic;
using Assets.Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Scripts.Controllers
{
    internal class VerticalMovementController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;
        [Inject] private GlobalSettings _settings;

        private readonly HashSet<MovableObject> _objects = new();

        private void OnEnable()
        {
            _messageBus.Subscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Subscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
            _messageBus.Subscribe<ResetMessage>(OnReset);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Unsubscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
            _messageBus.Unsubscribe<ResetMessage>(OnReset);
        }

        private void OnReset(ResetMessage obj)
        {
            _objects.Clear();
        }

        private void OnObjectCreated(ObjectCreatedMessage message)
        {
            if (message.Object.TryGetComponent(out MovableObject movable))
                _objects.Add(movable);
        }

        private void OnObjectDestroyed(ObjectDestroyedMessage message)
        {
            if (message.Object.TryGetComponent(out MovableObject movable))
                _objects.Remove(movable);
        }

        void Update()
        {
            float dy = _settings.ScrollSpeed * Time.deltaTime;

            foreach (var item in _objects)
            {
                if (!item.IsMovable)
                    continue;

                var pos = item.transform.localPosition;
                item.transform.localPosition = new Vector3(pos.x, pos.y + dy, pos.z);
            }
        }
    }
}
