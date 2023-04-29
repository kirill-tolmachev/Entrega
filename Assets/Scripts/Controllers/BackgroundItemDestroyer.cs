using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class BackgroundItemDestroyer : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        [SerializeField] private LayerMask _layerMask;

        private readonly List<GameObject> _destroyables = new List<GameObject>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("collision: " + other.gameObject.name);
            if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
                _destroyables.Add(other.gameObject);
        }

        private void LateUpdate()
        {
            for (int i = _destroyables.Count - 1; i >= 0; i--)
            {
                var item = _destroyables[i];
                _destroyables.RemoveAt(i);
                if (item)
                {
                    _messageBus.Publish(new ObjectDestroyedMessage(item.transform));
                    Destroy(item.gameObject);
                }
            }
        }

    }
}
