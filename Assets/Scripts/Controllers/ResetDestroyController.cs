using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class ResetDestroyController : MonoBehaviour
    {
        private HashSet<InGameObject> _objects = new HashSet<InGameObject>();
        [Inject] private IMessageBus _messageBus;


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


        private void OnObjectCreated(ObjectCreatedMessage message)
        {
            if (!message.Object.TryGetComponent(out InGameObject go))
                return;

            if (!go.DeleteOnReset)
                return;

            _objects.Add(go);
        }

        private void OnObjectDestroyed(ObjectDestroyedMessage message)
        {
            if (!message.Object.TryGetComponent(out InGameObject go))
                return;

            if (!go.DeleteOnReset)
                return;

            _objects.Remove(go);
        }

        private void OnReset(ResetMessage _)
        {
            foreach (var o in _objects.ToArray())
            {
                Destroy(o.gameObject);
            }

            _objects.Clear();
        }

    }
}
