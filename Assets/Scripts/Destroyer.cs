using System.Collections.Generic;
using Assets.Scripts.MessageImpl;
using Cysharp.Threading.Tasks;
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
            if (other.TryGetComponent(out Enemy enemy))
            {
                if (!enemy.IsDead)
                    return;

                var damageable = enemy.GetComponent<Damageable>();
                _messageBus.Publish(new DeathMessage(damageable));
            }

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
                    _messageBus.Publish(new ObjectDestroyedMessage(item.transform)).Forget();
                    Destroy(item.gameObject);
                }
            }
        }


    }
}