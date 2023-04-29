using System.Collections.Generic;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal class Destroyer : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        private readonly List<GameObject> _destroyables = new List<GameObject>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            _destroyables.Add(other.gameObject);
        }

        private void LateUpdate()
        {
            for (int i = _destroyables.Count - 1; i >= 0; i--)
            {
                var item = _destroyables[i];
                _destroyables.RemoveAt(i);
                _messageBus.Publish(new ObjectDestroyedMessage(item.transform));

                Destroy(item.gameObject);
            }
        }


    }
}